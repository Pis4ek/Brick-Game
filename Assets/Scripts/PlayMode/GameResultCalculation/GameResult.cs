using Services.Timer;

namespace PlayMode.GameResultCalculation
{
    public class GameResult
    {
        public int Score { get; private set; }
        public GameTime Time { get; private set; }
        public int LeaderboardPlace { get; private set; }


        public GameResult(int score, GameTime time, int leaderboardPlace)
        {
            Score = score;
            Time = time;
            LeaderboardPlace = leaderboardPlace;
        }

        public GameResult(GameResult gameResult)
        {
            Score = gameResult.Score;
            Time = gameResult.Time;
            LeaderboardPlace = gameResult.LeaderboardPlace;
        }

        public static bool operator >(GameResult a, GameResult b)
        {
            return a.Score > b.Score;
        }
        public static bool operator <(GameResult a, GameResult b)
        {
            return a.Score < b.Score;
        }
        public static bool operator >=(GameResult a, GameResult b)
        {
            return a.Score >= b.Score;
        }
        public static bool operator <=(GameResult a, GameResult b)
        {
            return a.Score <= b.Score;
        }
    }
}
