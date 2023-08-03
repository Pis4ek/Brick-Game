using Newtonsoft.Json;
using PlayMode.Bricks;
using PlayMode.Map;
using Services;
using Services.Storage;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode
{
    public class BrickSpawner : MonoBehaviour, IService
    {
        public event Action OnValueChangedEvent;
        public event Action OnBrickCanNotSpawnEvent;

        public BrickConfig NextBrick { get { return _configLoader.GetConfig(NextBrickIndex); } }
        public int NextBrickIndex { get; private set; }
        public int? SavedBrickIndex { get; private set; } = null;

        private BrickConfigLoader _configLoader;
        private IResetableBrick _brick;
        private Vector2Int _spawnPoint;
        private int _currentBrickIndex;

        public BrickSpawner Init(BlockMap blockMap, IResetableBrick brick, IGameStateEvents gameStateEvents)
        {
            _brick = brick;

            _configLoader = new BrickConfigLoader();

            blockMap.OnBrickLandedEvent += SpawnNewBrick;

            CalculateSpawnPoint(blockMap);

            gameStateEvents.OnGameStartedEvent += SpawnNewBrick;

            NextBrickIndex = _configLoader.GetRandomIndex();

            return this;
        }

        public void SaveCurrentBrick()
        {
            if(SavedBrickIndex != null)
            {
                int tmp = _currentBrickIndex;
                _currentBrickIndex = SavedBrickIndex.Value;
                SavedBrickIndex = tmp;

                _brick.ResetBrick(_spawnPoint, _configLoader.GetConfig(_currentBrickIndex));
                OnValueChangedEvent?.Invoke();
            }
            else
            {
                SavedBrickIndex = _currentBrickIndex;
                Spawn(_currentBrickIndex);
            }
        }

        private void Spawn(int index)
        {
            BrickConfig config = _configLoader.GetConfig(index);

            if (_brick.ResetBrick(_spawnPoint, config) == false)
            {
                OnBrickCanNotSpawnEvent?.Invoke();
            }
        }

        private void SpawnNewBrick()
        {
            _currentBrickIndex = NextBrickIndex;
            NextBrickIndex = _configLoader.GetRandomIndex();

            Spawn(_currentBrickIndex);
            OnValueChangedEvent?.Invoke();
        }

        private void CalculateSpawnPoint(BlockMap blockMap)
        {
            _spawnPoint = new Vector2Int(blockMap.MapSize.x / 2 - 2, 0);
        }
    }
}
