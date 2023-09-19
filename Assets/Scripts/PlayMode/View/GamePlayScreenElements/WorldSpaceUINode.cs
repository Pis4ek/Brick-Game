using LeaderboardComponents;
using PlayMode.BrickSpawnerElements;
using PlayMode.GameResultCalculation;
using PlayMode.Level;
using PlayMode.Score;
using Services.Timer;
using UnityEngine;

namespace PlayMode.View
{
    public class WorldSpaceUINode : MonoBehaviour, IUINode
    {
        [SerializeField] GameObject _shading;
        [SerializeField] GameObject _widgets;

        private GameObject _infoPanel;
        private GameObject _endGamePanel;
        private GameObject _brickPredication;
        private GameObject _holdButton;
        private GameObject _leaderboardView;

        public WorldSpaceUINode Init(BrickSpawningHolder brickHolder, BrickSpawnerData brickSpawnerData, 
            IReadOnlyScoreData scoreData, IReadOnlyTimerData timer, IReadOnlyLevelData level, 
            GameResultCalculator gameResult)
        {
            _infoPanel = GetComponentInChildren<InfoPanel>(true).Init(scoreData, timer, level).gameObject;
            _endGamePanel = GetComponentInChildren<GameResultView>(true).Init(gameResult).gameObject;
            _brickPredication = GetComponentInChildren<BrickPredicationView>(true).Init(brickSpawnerData).gameObject;
            _holdButton = GetComponentInChildren<BrickHolderInput>(true).Init(brickHolder).gameObject;
            _holdButton.GetComponent<BrickHolderView>().Init(brickHolder);
            _leaderboardView = GetComponentInChildren<LeaderboardView>(true).Init(true).gameObject;

            return this;
        }

        public void UpdateState(GameStateType gameState)
        {
            _endGamePanel.transform.Disactivate();
            _infoPanel.transform.Disactivate();
            _widgets.transform.Disactivate();
            _brickPredication.transform.Disactivate();
            _holdButton.transform.Disactivate();
            _shading.transform.Disactivate();
            _leaderboardView.transform.Disactivate();

            if (gameState == GameStateType.Ended)
            {
                _endGamePanel.transform.Activate();
            }
            else if (gameState == GameStateType.Paused)
            {
                _shading.transform.Activate();
                _leaderboardView.transform.Activate();
            }
            else
            {
                _infoPanel.transform.Activate();
                _widgets.transform.Activate();
                _brickPredication.transform.Activate();
                _holdButton.transform.Activate();
            }
        }
    }
}