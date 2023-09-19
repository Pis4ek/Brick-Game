using System;
using UniRx;

namespace PlayMode
{
    public interface IGameState
    {
        public event Action OnGameStartedEvent;

        public IReadOnlyReactiveProperty<GameStateType> State { get; }
    }
}
