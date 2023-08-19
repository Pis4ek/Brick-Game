using System;
using UnityEngine;

namespace PlayMode.BrickSpawnerElements
{
    public class BrickSpawningHolder
    {
        public event Action OnBrickHeldEvent;

        public ISpawnerData Data => _data;

        private BrickSpawner _brickSpawner;
        private BrickSpawnerData _data;

        public BrickSpawningHolder(BrickSpawner brickSpawner, BrickSpawnerData brickSpawnerData)
        {
            _brickSpawner = brickSpawner;
            _data = brickSpawnerData;
        }

        public void HoldCurrentBrick()
        {
            if (_data.HeldBrickIndex != null)
            {
                int tmp = _data.CurrentBrickIndex;
                _data.CurrentBrickIndex = _data.HeldBrickIndex.Value;
                _data.HeldBrickIndex = tmp;
                _brickSpawner.SpawnCurrentBrick();
                OnBrickHeldEvent?.Invoke();
            }
            else
            {
                _data.HeldBrickIndex = _data.CurrentBrickIndex;
                _brickSpawner.SpawnNextBrick();
                OnBrickHeldEvent?.Invoke();
            }
        }
    }
}