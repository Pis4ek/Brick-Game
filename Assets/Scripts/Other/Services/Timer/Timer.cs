using PlayMode;
using System;
using UnityEngine;

namespace Services.Timer
{
    public class Timer : MonoBehaviour, IService
    {
        public event Action OnSecondTickedEvent;
        public event Action OnMinuteTickedEvent;

        public float TimeSinceStart { get; private set; } = 0f;
        public int SecondsSinceStart { get; private set; } = 0;
        public int MinutesSinceStart { get; private set; } = 0;
        public GameTime GameTime => new GameTime(MinutesSinceStart, SecondsSinceStart);
        public bool TimerStarted { get; private set; } = false;

        public Timer Init(IGameStateEvents gameEvents)
        {
            gameEvents.OnGameStartedEvent += StartTimer;
            gameEvents.OnGameEndedEvent += StopTimer;

            return this;
        }


        private void FixedUpdate()
        {
            if (TimerStarted)
            {
                TimeSinceStart += Time.fixedDeltaTime;
                if (TimeSinceStart > SecondsSinceStart + 1)
                {
                    SecondsSinceStart++;
                    OnSecondTickedEvent?.Invoke();
                }
                if (TimeSinceStart / 60 > MinutesSinceStart + 1)
                {
                    MinutesSinceStart++;
                    OnMinuteTickedEvent?.Invoke();
                }
            }
        }

        private void StartTimer()
        {
            TimerStarted = true;
        }

        private void StopTimer()
        {
            TimerStarted = true;
        }
    }
}

