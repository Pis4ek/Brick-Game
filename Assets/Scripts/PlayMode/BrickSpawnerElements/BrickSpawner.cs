using PlayMode.Bricks;
using PlayMode.BrickSpawnerElements;
using PlayMode.Map;
using System;

namespace PlayMode
{
    public class BrickSpawner
    {
        public event Action OnBrickCanNotSpawnEvent;

        private BrickSpawnerData _data;
        private BrickSpawningPredicator _brickPredicator;
        private IResetableBrick _brick;
        private IGameStateEvents _gameState;

        public BrickSpawner(BrickSpawnerData data, IResetableBrick brick, 
            IReadOnlyBrickData brickData, IGameStateEvents gameStateEvents)
        {
            _brick = brick;
            _data = data;
            _gameState = gameStateEvents;
            _brickPredicator = new BrickSpawningPredicator(_data);

            brickData.OnBrickLandedEvent += SpawnNextBrick;
            gameStateEvents.OnGameStartedEvent += SpawnNextBrick;
        }

        public void SpawnCurrentBrick()
        {
            if(_gameState.State == GameState.Playing)
            {
                if (_brick.ResetBrick(_data.SpawnPoint, _data.CurrentBrick) == false)
                {
                    OnBrickCanNotSpawnEvent?.Invoke();
                }
            }
        }

        public void SpawnNextBrick()
        {
            _brickPredicator.SetNextBrickIndex();
            SpawnCurrentBrick();
        }
    }
}
