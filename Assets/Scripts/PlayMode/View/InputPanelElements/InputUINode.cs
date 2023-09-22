using PlayMode.Bricks;
using Services.Timer;
using UnityEngine;

namespace PlayMode.View
{
    public class InputUINode : MonoBehaviour, IUINode
    {
        private BrickInput _brickInput;
        private PauseInput _pauseInput;
        private PauseMenuButtonsInput _otherButtons;

        public InputUINode Init(IControllableBrick brick, IReadOnlyTimerData timer, IPauseControl pauseControl)
        {
            _brickInput = GetComponentInChildren<BrickInput>(true).Init(brick, timer);
            _pauseInput = GetComponentInChildren<PauseInput>(true).Init(pauseControl);
            _otherButtons = GetComponentInChildren<PauseMenuButtonsInput>(true).Init();

            return this;
        }

        public void UpdateState(GameStateType gameState)
        {
            if (gameState == GameStateType.Paused)
            {
                _otherButtons.Activate();
                _pauseInput.Activate();
                _brickInput.HideButtons();
            }
            else if(gameState == GameStateType.Ended)
            {
                _otherButtons.Activate();
                _brickInput.HideButtons();
                _pauseInput.Disactivate();
            }
            else 
            {
                _brickInput.ShowButtons();
                _pauseInput.Activate();
                _otherButtons.Disactivate();
            }
        }
    }
}