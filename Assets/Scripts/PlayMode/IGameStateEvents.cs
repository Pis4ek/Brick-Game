using System;

namespace PlayMode
{
    public interface IGameStateEvents
    {
        public event Action OnGameStartedEvent;
        public event Action OnGameEndedEvent;
    }
}
