using UnityEngine;

namespace PlayMode.View
{
    public class UIController
    {
        private ScreenSpaceUINode _downInputPanel;
        private WorldSpaceUINode _playInfoPanel;

        public UIController(IGameStateEvents gameStateEvents, ScreenSpaceUINode downInputPanel, WorldSpaceUINode playInfoPanel)
        {
            _downInputPanel = downInputPanel;
            _playInfoPanel = playInfoPanel;

            gameStateEvents.OnValueChangedEvent += delegate ()
            {
                _downInputPanel.UpdateState(gameStateEvents.State);
                _playInfoPanel.UpdateState(gameStateEvents.State);
            };
        }
    }
}