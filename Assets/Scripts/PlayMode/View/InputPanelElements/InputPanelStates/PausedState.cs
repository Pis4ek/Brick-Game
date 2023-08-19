using UnityEngine;

namespace PlayMode.View.InputPanelElements
{
    public class PausedState : IUIPanelState
    {
        private PauseMenuButtonsInput _otherObjects;
        private PauseInput _pauseInput;

        public PausedState(PauseMenuButtonsInput otherObjects, PauseInput pauseInput)
        {
            _otherObjects = otherObjects;
            _pauseInput = pauseInput;
        }

        public void Enter()
        {
            _otherObjects.Activate();
            _pauseInput.Activate();
        }

        public void Exit()
        {
            _otherObjects.Disactivate();
            _pauseInput.Disactivate();
        }
    }
}