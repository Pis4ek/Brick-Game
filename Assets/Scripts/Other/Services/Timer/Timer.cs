using PlayMode;
using UniRx;
using UnityEngine;

namespace Services.Timer
{
    public class Timer : MonoBehaviour
    {
        private TimerData _data;
        private CompositeDisposable disposables = new CompositeDisposable();

        public Timer Init(TimerData data, IGameState gameState)
        {
            _data = data;

            gameState.State.Subscribe(value =>
            {
                if (value == GameStateType.Playing)
                    _data.IsTimerStarted = true;
                else
                    _data.IsTimerStarted = false;
            }).AddTo(disposables);

            return this;
        }

        private void FixedUpdate()
        {
            if (_data.IsTimerStarted)
            {
                _data.timeSinceStart.Value += Time.fixedDeltaTime;
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
            }
        }
    }
}

