using System;
using System.Collections;
using UnityEngine;

namespace Services.Timer
{
    public class TimerData : IReadOnlyTimerData
    {
        public event Action OnFallingTimeTickedEvent;
        public event Action OnSecondTickedEvent;
        public event Action OnMinuteTickedEvent;

        public float FallingTimeStep { get; set; } = 1f;
        public float TimeSinceStart { get; set; } = 0f;
        public int SecondsSinceStart { get; set; } = 0;
        public int Seconds { get; set; } = 0;
        public int MinutesSinceStart { get; set; } = 0;
        public GameTime GameTime => new GameTime(MinutesSinceStart, Seconds);
        public bool IsTimerStarted { get; set; } = false;

        public void SendFallingTimeTick() => OnFallingTimeTickedEvent?.Invoke();
        public void SendSecondTick() => OnSecondTickedEvent?.Invoke();
        public void SendMinuteTick() => OnMinuteTickedEvent?.Invoke();
        
    }
}