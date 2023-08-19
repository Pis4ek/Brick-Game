using System;

namespace PlayMode.Level
{
    public interface IReadOnlyLevelData
    {
        public event Action OnValueChangedEvent;

        public int Level { get; }
    }
}
