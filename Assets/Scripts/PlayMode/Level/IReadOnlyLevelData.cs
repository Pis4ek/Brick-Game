using System;
using UniRx;

namespace PlayMode.Level
{
    public interface IReadOnlyLevelData
    {
        public IReadOnlyReactiveProperty<float> Level { get; }
    }
}
