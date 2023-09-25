using LeaderboardComponents;
using PlayMode.Score;
using Services;
using Services.Timer;
using System;
using UniRx;

namespace PlayMode.GameResultCalculation
{
    public class GameResultCalculator : IService
    {
        public event Action<GameResult> OnResultCalculated;

        private IReadOnlyTimerData _timer;
        private IReadOnlyScoreData _scoreData;
        private Leaderboard _leaderboard;

        public GameResultCalculator(IGameState gameEvents, IReadOnlyTimerData timer, 
            IReadOnlyScoreData scoreData, Leaderboard leaderboard)
        {
            gameEvents.State
                .Where(value => value == GameStateType.Ended)
                .Subscribe(value => { CalculateResult(); });

            _timer = timer;
            _scoreData = scoreData;
            _leaderboard = leaderboard;
        }

        private void CalculateResult()
        {
            GameResult result = new GameResult(_scoreData.Score.Value, _timer.GameTime);

            _leaderboard.TryAddGameResult(_scoreData.Score.Value, _timer.GameTime);

            OnResultCalculated?.Invoke(result);
        }
    }
}