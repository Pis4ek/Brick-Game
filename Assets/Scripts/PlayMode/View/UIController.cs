using UniRx;

namespace PlayMode.View
{
    public class UIController
    {
        private InputUINode _downInputPanel;
        private GamePlayUINode _playInfoPanel;

        public UIController(IGameState gameStateEvents, InputUINode downInputPanel, GamePlayUINode playInfoPanel)
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