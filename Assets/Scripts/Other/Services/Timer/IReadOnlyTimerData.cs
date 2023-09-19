using System;
using UniRx;

namespace Services.Timer
{
    public interface IReadOnlyTimerData
    {
        public event Action OnFallingTimeTickedEvent;

        public IReadOnlyReactiveProperty<float> FallingTimeStep { get; }
        public IReadOnlyReactiveProperty<float> TimeSinceStart { get; }
        public IReadOnlyReactiveProperty<int> SecondsSinceStart { get; }
        public IReadOnlyReactiveProperty<int> Seconds { get; }
        public IReadOnlyReactiveProperty<int> MinutesSinceStart { get; }
        public GameTime GameTime { get; }
        public bool IsTimerStarted { get; }
    }
}
