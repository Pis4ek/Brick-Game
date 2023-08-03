namespace Services.Timer
{
    public struct GameTime
    {
        public int Minutes;
        public int Seconds;

        public GameTime(int minutes, int seconds)
        {
            Minutes = minutes;
            Seconds = seconds;
        }

        public override string ToString()
        {
            return $"{Minutes}:{Seconds}";
        }
    }
}
