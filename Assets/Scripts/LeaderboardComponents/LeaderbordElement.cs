using PlayMode.GameResultCalculation;
using Services.Timer;
using System;

namespace LeaderboardComponents
{
    [Serializable]
    public class LeaderbordElement : GameResult
    {
        public DateTime Date { get; private set; }

        public LeaderbordElement(int score, GameTime time, int leaderboardPlace, DateTime date) 
            : base(score, time, leaderboardPlace)
        {
            Date = date;
        }

        public LeaderbordElement(GameResult gameResult, DateTime date)
            : base(gameResult)
        {
            Date = date;
        }
    }
}
