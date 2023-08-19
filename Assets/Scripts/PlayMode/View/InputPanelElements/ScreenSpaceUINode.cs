using LeaderboardComponents;
using PlayMode.Bricks;
using PlayMode.View.InputPanelElements;
using Services.Timer;
using UnityEngine;

namespace PlayMode.View
{
    public class ScreenSpaceUINode : MonoBehaviour, IUINode
    {
        public PauseInput PauseInput => _pauseInput;

        private IUIPanelState _currentState;
        private BrickInput _brickInput;
        private PauseInput _pauseInput;
        private PauseMenuButtonsInput _otherButtons;

        public ScreenSpaceUINode Init(IControllableBrick brick, IReadOnlyTimerData timer)
        {
            _brickInput = GetComponentInChildren<BrickInput>(true).Init(brick, timer);
            _pauseInput = GetComponentInChildren<PauseInput>(true).Init();
            _otherButtons = GetComponentInChildren<PauseMenuButtonsInput>(true).Init();

            _otherButtons.Disactivate();

            _currentState = new PlayState(_brickInput, _pauseInput);

            return this;
        }

        public void UpdateState(GameState gameState)
        {
            _currentState.Exit();
            if (gameState == GameState.Paused)
            {
                _currentState = new PausedState(_otherButtons, _pauseInput);
            }
            else if(gameState == GameState.Ended)
            {
                _currentState = new EndGameState(_otherButtons);
            }
            else 
            {
                _currentState = new PlayState(_brickInput, _pauseInput);
            }
            _currentState.Enter();
        }
    }
}