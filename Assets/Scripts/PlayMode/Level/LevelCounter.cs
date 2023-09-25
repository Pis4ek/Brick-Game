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


        public LevelCounter(LevelData data, IReadOnlyScoreData scoreData, IReadOnlyTimerData timerData)
        {
            _data = data;
            _scoreData = scoreData;

            timerData.MinutesSinceStart
                .Where(value => _data.level.Value <= 40)
                .Subscribe(value => { _data.level.Value++; })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}