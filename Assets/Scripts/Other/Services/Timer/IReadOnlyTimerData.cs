using System;

namespace Services.Timer
{
    public interface IReadOnlyTimerData
    {
        public event Action OnFallingTimeTickedEvent;
        public event Action OnSecondTickedEvent;
        public event Action OnMinuteTickedEvent;

        public float FallingTimeStep { get; }
        public float TimeSinceStart { get; }
        public int SecondsSinceStart { get; }
        public int Seconds { get; }
        public int MinutesSinceStart { get; }
        public GameTime GameTime { get; }
        public bool IsTimerStarted { get; }
    }
}
