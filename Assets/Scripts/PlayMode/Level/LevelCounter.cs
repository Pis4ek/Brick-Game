using PlayMode.Score;
using UnityEngine;

namespace PlayMode.Level
{
    public class LevelCounter
    {
        private LevelData _data;
        private IReadOnlyScoreData _scoreData;
        private int _currentLevelLimit = 1000;


        public LevelCounter(LevelData data, IReadOnlyScoreData scoreData)
        {
            _data = data;
            _scoreData = scoreData;

            _scoreData.OnValueChangedEvent += CheckLevelUp;
        }

        private void CheckLevelUp()
        {
            if(_scoreData.Score > _currentLevelLimit)
            {
                _currentLevelLimit *= 2;
                _data.Level++;
            }
        }
    }
}