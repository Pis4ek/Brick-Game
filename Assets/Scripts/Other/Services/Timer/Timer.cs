using PlayMode;
using PlayMode.Level;
using System;
using UnityEngine;

namespace Services.Timer
{
    public class Timer : MonoBehaviour, IService
    {
        private TimerData _data;
        private float _fallingTimeCounter = 0;
        private IReadOnlyLevelData _levelData;

        public Timer Init(TimerData data, IReadOnlyLevelData levelData, IGameStateEvents gameEvents)
        {
            gameEvents.OnGameStartedEvent += StartTimer;
            gameEvents.OnGameEndedEvent += StopTimer;
            gameEvents.OnGameUnpausedEvent += StartTimer;
            gameEvents.OnGamePausedEvent += StopTimer;

            _levelData = levelData;
            levelData.OnValueChangedEvent += SetLevelTimeStep;

            _data = data;

            return this;
        }

        private void FixedUpdate()
        {
            if (_data.IsTimerStarted)
            {
                _data.TimeSinceStart += Time.fixedDeltaTime;
                _fallingTimeCounter += Time.fixedDeltaTime;
                if (_data.TimeSinceStart > _data.SecondsSinceStart + 1)
                {
                    _data.SecondsSinceStart++;
                    _data.Seconds++;
                    if(_data.Seconds >= 60)
                    {
                        _data.Seconds = 0;
                        _data.MinutesSinceStart++;
                        _data.SendMinuteTick();
                    }
                    _data.SendSecondTick();
                }
                if (_data.TimeSinceStart / 60 > _data.MinutesSinceStart + 1)
                {
                }
                if(_fallingTimeCounter > _data.FallingTimeStep)
                {
                    _fallingTimeCounter -= _data.FallingTimeStep;
                    _data.SendFallingTimeTick();
                }
            }
        }

        private void StartTimer()
        {
            _data.IsTimerStarted = true;
        }

        private void StopTimer()
        {
            _data.IsTimerStarted = false;
        }

        private void SetLevelTimeStep()
        {
            _data.FallingTimeStep = 1 - 0.05f * _levelData.Level;
        }
    }
}

