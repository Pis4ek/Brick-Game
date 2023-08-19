using System;

namespace PlayMode.Level
{
    public class LevelData : IReadOnlyLevelData
    {
        public event Action OnValueChangedEvent;

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnValueChangedEvent?.Invoke();
            }
        }

        private int _level = 1;
    }
}