using MyRx;
using PlayMode.Map;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class Brick : IResetableBrick, IControllableBrick
    {
        public event Action OnResetedEvent;
        public event Action<BrickAnimationType> OnMovedEvent;
        public event Action OnCanNotFall;
        public event Action<List<Vector2Int>> OnFullDownPositionUpdatedEvent;
        public event Action OnBrickLandedEvent;

        public IReadOnlyBrickShape Shape => _shape;
        public ReactiveProperty<bool> IsLanded = new ReactiveProperty<bool>();

        private BrickShape _shape  = new BrickShape();
        private DownPositionCalculator _downPositionCalculator;
        private BrickRotator _rotator;
        private BrickMover _mover;
        private BrickReseter _reseter;
        private BlockMap _map;

        public Brick(BlockMap map)
        {
            _map = map;
            _rotator = new BrickRotator(_shape, map);
            _mover = new BrickMover(_shape, map);
            _reseter = new BrickReseter(_shape, map);
            _downPositionCalculator = new DownPositionCalculator(_shape, map);
        }

        public bool ResetBrick(Vector2Int startCoordiantes, BrickConfig config)
        {
            IsLanded.Value = false;
            if (_reseter.Reset(startCoordiantes, config))
            {
                _shape.Color = config.Color;
                OnResetedEvent?.Invoke();
                var fullDownPosition = _downPositionCalculator.RecalculateFullDownPosition();
                OnFullDownPositionUpdatedEvent?.Invoke(fullDownPosition);
                return true;
            }
            return false;
        }

        public bool DownMove()
        {
            if (IsLanded.Value == false)
            {
                if (_mover.Move(Vector2Int.down))
                {
                    OnMovedEvent?.Invoke(BrickAnimationType.Down);
                    return true;
                }
                else
                {
                    IsLanded.Value = true;
                    OnCanNotFall?.Invoke();
                }
            }

            return false;
        }

        public bool FullDownMove()
        {
            if (IsLanded.Value == false)
            {
                _downPositionCalculator.FallToDown();
                IsLanded.Value = true;
                OnMovedEvent?.Invoke(BrickAnimationType.FullDown);
                return true;
            }
            return false;
        }

        public bool LeftMove()
        {
            if (IsLanded.Value == false)
            {
                if (_mover.Move(Vector2Int.left))
                {
                    var fullDownPosition = _downPositionCalculator.RecalculateFullDownPosition();
                    OnFullDownPositionUpdatedEvent?.Invoke(fullDownPosition);
                    OnMovedEvent?.Invoke(BrickAnimationType.Side);
                    return true;
                }
            }
            return false;
        }

        public bool RightMove()
        {
            if (IsLanded.Value == false)
            {
                if (_mover.Move(Vector2Int.right))
                {
                    var fullDownPosition = _downPositionCalculator.RecalculateFullDownPosition();
                    OnFullDownPositionUpdatedEvent?.Invoke(fullDownPosition);
                    OnMovedEvent?.Invoke(BrickAnimationType.Side);
                    return true;
                }
            }
            return false;
        }

        public bool Rotate()
        {
            if (IsLanded.Value == false)
            {
                if (_rotator.Rotate())
                {
                    var fullDownPosition = _downPositionCalculator.RecalculateFullDownPosition();
                    OnFullDownPositionUpdatedEvent?.Invoke(fullDownPosition);
                    OnMovedEvent?.Invoke(BrickAnimationType.Rotate);
                    return true;
                }
            }

            return false;
        }

        public void SendBrickLandedEvent()
        {
            //IsLanded.Value = false;
            _map.AddBrick(_shape);
            OnBrickLandedEvent?.Invoke();
        }
    }
}