using UnityEngine;

namespace PlayMode.View.GamePlayScreenElements
{
    public class PlayState : IUIPanelState
    {
        private GameObject _infoPanel;
        private GameObject _widgets;
        private GameObject _brickPredication;
        private GameObject _holdButton;

        public PlayState(GameObject infoPanel, GameObject widgets, GameObject brickPredication, GameObject holdButton)
        {
            _infoPanel = infoPanel;
            _widgets = widgets;
            _brickPredication = brickPredication;
            _holdButton = holdButton;
        }

        public void Enter()
        {
            _infoPanel.transform.Activate();
            _widgets.transform.Activate();
            _brickPredication.transform.Activate();
            _holdButton.transform.Activate();
        }

        public void Exit()
        {
            _infoPanel.transform.Disactivate();
            _widgets.transform.Disactivate();
            _brickPredication.transform.Disactivate();
            _holdButton.transform.Disactivate();
        }
    }
}