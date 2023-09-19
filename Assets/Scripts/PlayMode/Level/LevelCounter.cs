using PlayMode.Score;
using Services.Timer;
using System;
using UniRx;

namespace PlayMode.Level
{
    public class LevelCounter : IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        private LevelData _data;
        private IReadOnlyScoreData _scoreData;
        private int _currentLevelLimit = 10;


        public LevelCounter(LevelData data, IReadOnlyScoreData scoreData, IReadOnlyTimerData timerData)
        {
            _data = data;
            _scoreData = scoreData;

            //_scoreData.OnValueChangedEvent += CheckLevelUp;
            timerData.MinutesSinceStart
                .Where(value => value <= 40)
                .Subscribe(value => { _data.level.Value = value; })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Clear();
        }

        private void CheckLevelUp()
        {
            if(_scoreData.Score > _currentLevelLimit)
            {
                _currentLevelLimit *= 2;
                _data.level.Value++;
            }
        }
    }
}