using PlayMode;
using PlayMode.Level;
using System;
using UniRx;
using UnityEngine;

namespace Services.Timer
{
    public class Timer : MonoBehaviour, IService
    {
        private TimerData _data;
        private float _fallingTimeCounter = 0;
        private CompositeDisposable disposables = new CompositeDisposable();

        public Timer Init(TimerData data, IReadOnlyLevelData levelData, IGameState gameState)
        {
            _data = data;

            gameState.State.Subscribe(value =>
            {
                if (value == GameStateType.Playing)
                    _data.IsTimerStarted = true;
                else
                    _data.IsTimerStarted = false;
            }).AddTo(disposables);

            levelData.Level.Subscribe(value => { _data.fallingTimeStep.Value = 1 - 0.02f * value; }).AddTo(disposables);

            return this;
        }

        private void FixedUpdate()
        {
            if (_data.IsTimerStarted)
            {
                _data.timeSinceStart.Value += Time.fixedDeltaTime;
                _fallingTimeCounter += Time.fixedDeltaTime;
                if (_data.timeSinceStart.Value > _data.secondsSinceStart.Value + 1)
                {
                    _data.secondsSinceStart.Value++;
                    _data.seconds.Value++;
                    if(_data.seconds.Value >= 60)
                    {
                        _data.seconds.Value = 0;
                        _data.minutesSinceStart.Value++;
                    }
                }
                if (_data.timeSinceStart.Value / 60 > _data.minutesSinceStart.Value + 1)
                {
                }
                if(_fallingTimeCounter > _data.fallingTimeStep.Value)
                {
                    _fallingTimeCounter -= _data.fallingTimeStep.Value;
                    _data.SendFallingTimeTick();
                }
            }
        }
    }
}

