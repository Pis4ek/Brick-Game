using Services.Timer;
using System;

namespace PlayMode.GameResultCalculation
{
    [Serializable]
    public class GameResult
    {
        public int Score { get; private set; }
        public GameTime Time { get; private set; }
        public DateTime Date { get; private set; }


        public GameResult(int score, GameTime time)
        {
            Score = score;
            Time = time;
            Date = DateTime.Now;
        }

        public GameResult(GameResult gameResult)
        {
            Score = gameResult.Score;
            Time = gameResult.Time;
            Date = gameResult.Date;
        }
    }
}
