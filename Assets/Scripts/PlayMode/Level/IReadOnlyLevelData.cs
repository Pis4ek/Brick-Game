using System;
using UniRx;

namespace PlayMode.Level
{
    public interface IReadOnlyLevelData
    {
        public IReadOnlyReactiveProperty<int> Level { get; }
    }
}
