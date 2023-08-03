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

        private Timer _timer;
        private ScoreCounter _scoreCounter;
        private Leaderboard _leaderboard;

        public GameResultCalculator(IGameStateEvents gameEvents, Timer timer, 
            ScoreCounter scoreCounter, Leaderboard leaderboard)
        {
            gameEvents.OnGameEndedEvent += CalculateResult;
            _timer = timer;
            _scoreCounter = scoreCounter;
            _leaderboard = leaderboard;
        }

        private void CalculateResult()
        {
            GameResult result = new GameResult(_scoreCounter.Score, _timer.GameTime, 0);

            if (_leaderboard.TryAddGameResult(_scoreCounter.Score, _timer.GameTime, out var r))
            {
                result = r;
            }

            OnResultCalculated?.Invoke(result);
        }
    }
}