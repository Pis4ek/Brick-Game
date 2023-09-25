using System;
using UniRx;

namespace PlayMode.Score
{
    public interface IReadOnlyScoreData
    {
        /*public event Action OnValueChangedEvent;

        public int Score { get; }*/

        public IReadOnlyReactiveProperty<int> Score { get; }
    }
}
