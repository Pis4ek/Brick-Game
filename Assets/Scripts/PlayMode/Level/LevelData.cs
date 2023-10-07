using System;
using UniRx;

namespace PlayMode.Level
{
    public class LevelData : IReadOnlyLevelData
    {
        public ReactiveProperty<float> level = new ReactiveProperty<float>(0);
        public IReadOnlyReactiveProperty<float> Level => level;
    }
}