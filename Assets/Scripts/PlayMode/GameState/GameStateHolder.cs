using System;
using UniRx;

namespace PlayMode
{
    public class GameStateHolder : IGameState, IPauseControl, IGameEndingControl
    {
        public event Action OnGameStartedEvent;

        public IReadOnlyReactiveProperty<GameStateType> State => state;

        public ReactiveProperty<GameStateType> state = new ReactiveProperty<GameStateType>(GameStateType.Uninitialized);

        public void SetPause()
        {
            if (state.Value == GameStateType.Playing)
            {
                state.Value = GameStateType.Paused;
            }
            else if (state.Value == GameStateType.Paused)
            {
                state.Value = GameStateType.Playing;
            }
        }

        public void SetPause(bool isPaused)
        {
            if (state.Value == GameStateType.Playing)
            {
                if (isPaused == true)
                {
                    state.Value = GameStateType.Paused;
                }
            }
            else if (state.Value == GameStateType.Paused)
            {
                if (isPaused == false)
                {
                    state.Value = GameStateType.Paused;
                }
            }
        }

        public void TryEndGame()
        {
            state.Value = GameStateType.Ended;
        }

        public void SendStartGame()
        {
            OnGameStartedEvent?.Invoke();
        }
    }
}