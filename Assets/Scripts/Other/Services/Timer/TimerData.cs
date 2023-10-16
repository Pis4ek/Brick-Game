using System;
using UniRx;

namespace Services.Timer
{
    public class TimerData : IReadOnlyTimerData
    {
        public IReadOnlyReactiveProperty<float> TimeSinceStart => timeSinceStart;
        public IReadOnlyReactiveProperty<int> SecondsSinceStart => secondsSinceStart;
        public IReadOnlyReactiveProperty<int> Seconds => seconds;
        public IReadOnlyReactiveProperty<int> MinutesSinceStart => minutesSinceStart;
        public GameTime GameTime => new GameTime(minutesSinceStart.Value, seconds.Value);
        public bool IsTimerStarted { get; set; } = false;

        public ReactiveProperty<float> fallingTimeStep = new ReactiveProperty<float>(1f);
        public ReactiveProperty<float> timeSinceStart = new ReactiveProperty<float>(0f);
        public ReactiveProperty<int> secondsSinceStart = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> seconds = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> minutesSinceStart = new ReactiveProperty<int>(0);
        
    }
}