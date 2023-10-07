using PlayMode.Score;
using Services.Timer;
using System;
using UniRx;

namespace PlayMode.Level
{
    public class LevelCounter
    {
        private LevelData _data;
        private IReadOnlyScoreData _scoreData;
        private IReadOnlyTimerData _timerData;
        private GameComplexity _complexity;

        public LevelCounter(LevelData data, IReadOnlyScoreData scoreData, IReadOnlyTimerData timerData,
            PlayModeConfig playModeConfig)
        {
            _data = data;
            _scoreData = scoreData;
            _timerData = timerData;
            _complexity = playModeConfig.complexity;

            timerData.MinutesSinceStart
                .Where(value => _data.level.Value < 100f)
                .Subscribe(value => { RecalculateLevel(); });

            scoreData.Score
                .Where(value => _data.level.Value < 100f)
                .Subscribe(value => { RecalculateLevel(); });


        }

        public void RecalculateLevel()
        {
            var minutes = _timerData.MinutesSinceStart.Value;
            var score = _scoreData.Score.Value;

            switch (_complexity)
            {
                case GameComplexity.Easy:
                    _data.level.Value = 2.5f * minutes + 0.003333f * score;
                    break;
                case GameComplexity.Difficult:
                    _data.level.Value = 5 * minutes + 0.01f * score;
                    break;
                default:
                    _data.level.Value = 3.333f * minutes + 0.005f * score;
                    break;
            }

            if(_data.level.Value > 100f)
            {
                _data.level.Value = 100f;
            }  
        }
    }
}