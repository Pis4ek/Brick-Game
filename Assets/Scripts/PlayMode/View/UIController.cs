using UniRx;

namespace PlayMode.View
{
    public class UIController
    {
        private ScreenSpaceUINode _downInputPanel;
        private WorldSpaceUINode _playInfoPanel;

        public UIController(IGameState gameStateEvents, ScreenSpaceUINode downInputPanel, WorldSpaceUINode playInfoPanel)
        {
            _downInputPanel = downInputPanel;
            _playInfoPanel = playInfoPanel;

            gameStateEvents.State.Subscribe(value => {
                _downInputPanel.UpdateState(gameStateEvents.State.Value);
                _playInfoPanel.UpdateState(gameStateEvents.State.Value);
            });
        }
    }
}