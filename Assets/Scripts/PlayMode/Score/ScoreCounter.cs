using PlayMode.Map;
using Services;
using Services.Timer;
using System;

namespace PlayMode.Score
{
    public class ScoreCounter : IService
    {
        public event Action OnValueChangedEvent;

        public int Score {
            get { return _score; }
            private set {
                _score = value;
                OnValueChangedEvent?.Invoke();
            } }

        private int _score  = 0;

        public ScoreCounter(BlockMap blockMap, Timer timer)
        {
            timer.OnSecondTickedEvent += delegate () { Score += 1; };

            blockMap.OnBlocksAddedEvent += delegate (int count) { Score += 2 * count; };

            blockMap.OnLinesDestroyedEvent += delegate (int count) { Score += 100 * count * count; };
        }
    }
}