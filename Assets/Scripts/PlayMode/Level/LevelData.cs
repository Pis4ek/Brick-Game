using System;
using UniRx;

namespace PlayMode.Level
{
    public class LevelData : IReadOnlyLevelData
    {
        public ReactiveProperty<int> level = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> Level => level;
    }
}