using LeaderboardComponents;
using PlayMode.Score;
using Services;
using Services.Timer;
using System;
using UnityEngine;

namespace PlayMode.GameResultCalculation
{
    public class GameResultCalculator : IService
    {
        public event Action<GameResult> OnResultCalculated;

        private IReadOnlyTimerData _timer;
        private IReadOnlyScoreData _scoreData;
        private Leaderboard _leaderboard;

        public GameResultCalculator(IGameStateEvents gameEvents, IReadOnlyTimerData timer, 
            IReadOnlyScoreData scoreData, Leaderboard leaderboard)
        {
            gameEvents.OnGameEndedEvent += CalculateResult;
            _timer = timer;
            _scoreData = scoreData;
            _leaderboard = leaderboard;
        }

        private void CalculateResult()
        {
            GameResult result = new GameResult(_scoreData.Score, _timer.GameTime);

            _leaderboard.TryAddGameResult(_scoreData.Score, _timer.GameTime);

            OnResultCalculated?.Invoke(result);
        }
    }
}