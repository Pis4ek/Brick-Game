using System;

namespace PlayMode
{
    public interface IGameStateEvents
    {
        public event Action OnValueChangedEvent;
        public event Action OnGameStartedEvent;
        public event Action OnGameEndedEvent;
        public event Action OnGamePausedEvent;
        public event Action OnGameUnpausedEvent;

        public GameState State { get; }
    }
}
