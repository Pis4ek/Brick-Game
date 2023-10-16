using PlayMode.Bricks;
using PlayMode.Level;
using System;
using UniRx;
using UnityEngine;

namespace PlayMode
{
    public class FallingTimeCounter : MonoBehaviour
    {
        public event Action OnFallingStepTicked;

        public IReadOnlyReactiveProperty<float> FallingTimeStep => _timeStep;
        public bool IsWorking { get; private set; }

        private ReactiveProperty<float> _timeStep = new ReactiveProperty<float>();
        private Brick _brick;
        private float _fallingTimeCounter;

        public FallingTimeCounter Init(Brick brick, IReadOnlyLevelData levelData, IGameState gameState)
        {
            _brick = brick;
            _brick.OnCanNotFall += Stop;
            _brick.OnResetedEvent += StartAndReset;
            levelData.Level.Subscribe(value => { UpdateFallingTimeStep(value); });

            gameState.State.Subscribe(value =>
            {
                if (value == GameStateType.Playing)
                    IsWorking = true;
                else
                    IsWorking = false;
            });
            return this;
        }

        private void FixedUpdate()
        {
            if (IsWorking)
            {
                _fallingTimeCounter += Time.fixedDeltaTime;
                if (_fallingTimeCounter > FallingTimeStep.Value)
                {
                    _fallingTimeCounter -= FallingTimeStep.Value;
                    OnFallingStepTicked?.Invoke();
                }
            }
        }

        private void UpdateFallingTimeStep(float level)
        {
            _timeStep.Value = 1 - 0.08f * level;
        }

        private void StartAndReset()
        {
            IsWorking = true;
            _fallingTimeCounter = 0;
        }

        private void Stop()
        {
            IsWorking = false;
        }
    }
}