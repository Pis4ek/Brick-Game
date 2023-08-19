using LeaderboardComponents;
using PlayMode.BrickSpawnerElements;
using PlayMode.GameResultCalculation;
using PlayMode.Level;
using PlayMode.Score;
using PlayMode.View.GamePlayScreenElements;
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

        private IUIPanelState _currentState;

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

            _currentState = new PlayState(_infoPanel, _widgets, _brickPredication, _holdButton);
            return this;
        }

        public void UpdateState(GameState gameState)
        {
            _currentState.Exit();
            if (gameState == GameState.Ended)
            {
                _currentState = new EndGameState(_endGamePanel);
            }
            else if (gameState == GameState.Paused)
            {
                _currentState = new PauseState(_infoPanel, _widgets, _brickPredication, _shading, _leaderboardView);
            }
            else
            {
                _currentState = new PlayState(_infoPanel, _widgets, _brickPredication, _holdButton);
            }
            _currentState.Enter();
        }
    }
}