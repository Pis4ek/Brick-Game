using LeaderboardComponents;
using PlayMode.BrickSpawnerElements;
using PlayMode.GameResultCalculation;
using PlayMode.Level;
using PlayMode.Score;
using Services.Timer;
using UnityEngine;

namespace PlayMode.View
{
    public class GamePlayUINode : MonoBehaviour, IUINode
    {
        [SerializeField] GameObject _shading;

        private GameObject _endGamePanel;
        private GameObject _leaderboardView;
        private SidePanels _sidePanels;

        public GamePlayUINode Init(BrickSpawningHolder brickHolder, BrickSpawnerData brickSpawnerData, 
            IReadOnlyScoreData scoreData, IReadOnlyTimerData timer, IReadOnlyLevelData level, 
            GameResultCalculator gameResult)
        {
            _endGamePanel = GetComponentInChildren<GameResultView>(true).Init(gameResult).gameObject;
            _leaderboardView = GetComponentInChildren<LeaderboardView>(true).Init(true).gameObject;

            _sidePanels = GetComponentInChildren<SidePanels>(true).Init(brickHolder, brickSpawnerData, scoreData, timer, level);

            return this;
        }

        public void UpdateState(GameStateType gameState)
        {
            _endGamePanel.transform.Disactivate();
            _shading.transform.Disactivate();
            _leaderboardView.transform.Disactivate();

            if (gameState == GameStateType.Ended)
            {
                _endGamePanel.transform.Activate();

                _sidePanels.Hide();
            }
            else if (gameState == GameStateType.Paused)
            {
                _shading.transform.Activate();
                _leaderboardView.transform.Activate();

                _sidePanels.Hide();
            }
            else
            {
                _sidePanels.Show();
            }
        }
    }
}