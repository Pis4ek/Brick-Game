using UnityEngine;

namespace PlayMode.View.GamePlayScreenElements
{
    public class PauseState : IUIPanelState
    {
        private GameObject _infoPanel;
        private GameObject _widgets;
        private GameObject _brickPredication;
        private GameObject _shading;
        private GameObject _leaderboardView;

        public PauseState(GameObject infoPanel, GameObject widgets, GameObject brickPredication, 
            GameObject shading, GameObject leaderboardView)
        {
            _infoPanel = infoPanel;
            _widgets = widgets;
            _brickPredication = brickPredication;
            _shading = shading;
            _leaderboardView = leaderboardView;
        }

        public void Enter()
        {
            _infoPanel.transform.Activate();
            _widgets.transform.Activate();
            _brickPredication.transform.Activate();
            _shading.transform.Activate();
            _leaderboardView.transform.Activate();
        }

        public void Exit()
        {
            _infoPanel.transform.Disactivate();
            _widgets.transform.Disactivate();
            _brickPredication.transform.Disactivate();
            _shading.transform.Disactivate();
            _leaderboardView.transform.Disactivate();
        }
    }
}