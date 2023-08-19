using UnityEngine;

namespace PlayMode.View.InputPanelElements
{
    public class EndGameState : IUIPanelState
    {
        private PauseMenuButtonsInput _otherObjects;

        public EndGameState(PauseMenuButtonsInput otherObjects)
        {
            _otherObjects = otherObjects;
        }

        public void Enter()
        {
            _otherObjects.Activate();
        }

        public void Exit()
        {
            _otherObjects.Disactivate();
        }
    }
}