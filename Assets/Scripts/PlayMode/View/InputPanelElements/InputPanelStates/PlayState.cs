using PlayMode.Bricks;
using UnityEngine;

namespace PlayMode.View.InputPanelElements
{
    public class PlayState : IUIPanelState
    {
        private BrickInput _brickInput;
        private PauseInput _pauseInput;

        public PlayState(BrickInput brickInput, PauseInput pauseInput)
        {
            _brickInput = brickInput;
            _pauseInput = pauseInput;
        }

        public void Enter()
        {
            _brickInput.Activate();
            _pauseInput.Activate();
        }

        public void Exit()
        {
            _brickInput.Disactivate();
            _pauseInput.Disactivate();
        }
    }
}
