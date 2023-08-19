using System;

namespace PlayMode.Score
{
    public interface IReadOnlyScoreData
    {
        public event Action OnValueChangedEvent;

        public int Score { get; }
    }
}
