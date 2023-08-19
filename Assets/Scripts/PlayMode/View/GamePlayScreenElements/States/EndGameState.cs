using UnityEngine;

namespace PlayMode.View.GamePlayScreenElements
{
    public class EndGameState : IUIPanelState
    {
        private GameObject _endGamePanel;

        public EndGameState(GameObject endGamePanel)
        {
            _endGamePanel = endGamePanel;
        }

        public void Enter()
        {
            _endGamePanel.transform.Activate();
        }

        public void Exit()
        {
            _endGamePanel.transform.Disactivate();
        }
    }
}