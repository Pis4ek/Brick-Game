using PlayMode.Bricks;
using PlayMode.BrickSpawnerElements;
using PlayMode.Map;
using System;
using UniRx;

namespace PlayMode
{
    public class BrickSpawner
    {
        private BrickSpawnerData _data;
        private BrickSpawningPredicator _brickPredicator;
        private IResetableBrick _brick;
        private IGameState _gameState;
        private IGameEndingControl _gameEndingControl;

        public BrickSpawner(BrickSpawnerData data, IResetableBrick resetableBrick, 
            IGameState gameState, IGameEndingControl gameEndingControl, Brick brick)
        {
            _brick = resetableBrick;
            _data = data;
            _gameState = gameState;
            _brickPredicator = new BrickSpawningPredicator(_data);
            _gameEndingControl = gameEndingControl;

            brick.OnBrickLandedEvent += SpawnNextBrick;
            gameState.OnGameStartedEvent += SpawnNextBrick;
        }

        public void SpawnCurrentBrick()
        {
            if(_gameState.State.Value == GameStateType.Playing)
            {
                if (_brick.ResetBrick(_data.SpawnPoint, _data.CurrentBrick) == false)
                {
                    _gameEndingControl.TryEndGame();
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
