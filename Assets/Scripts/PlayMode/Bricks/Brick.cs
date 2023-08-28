using PlayMode.Map;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class Brick : IResetableBrick, IControllableBrick
    {
        public event Action OnResetedEvent;
        public event Action<float> OnMovedEvent;
        public event Action OnCanNotFall;
        public event Action OnFullDownMovedEvent;
        public event Action OnFullDownPositionUpdatedEvent;

        private BrickRotator _rotator;
        private BrickMover _mover;
        private BrickReseter _reseter;
        private BlockMap _map;
        private BrickData _data;
        private DownPositionCalculator _downPositionCalculator;

        public Brick(BlockMap map, BrickData data)
        {
            _map = map;
            _data = data;
            _rotator = new BrickRotator(_data, map);
            _mover = new BrickMover(_data, map);
            _reseter = new BrickReseter(_data, map);
            _downPositionCalculator = new DownPositionCalculator(_data, map);

            _data.OnBrickLandedEvent += () => _map.AddBrick(_data.Shape);
        }

        public bool ResetBrick(Vector2Int startCoordiantes, BrickConfig config)
        {
            if(_reseter.Reset(startCoordiantes, config))
            {
                OnResetedEvent?.Invoke();
                _downPositionCalculator.RecalculateFullDownPosition();
                OnFullDownPositionUpdatedEvent?.Invoke();
                return true;
            }
            return false;
        }

        public bool DownMove()
        {
            if (_data.IsLanded == false)
            {
                if (_mover.Move(Vector2Int.down))
                {
                    OnMovedEvent?.Invoke(0.15f);
                    return true;
                }
                else
                {
                    _data.IsLanded = true;
                    OnCanNotFall?.Invoke();
                }
            }

            return false;
        }

        public bool FullDownMove()
        {
            if (_data.IsLanded == false)
            {
                _downPositionCalculator.FallToDown();
                _data.IsLanded = true;
                OnMovedEvent?.Invoke(0.15f);
                return true;
            }
            return false;
        }

        public bool LeftMove()
        {
            if (_data.IsLanded == false)
            {
                if (_mover.Move(Vector2Int.left))
                {
                    _downPositionCalculator.RecalculateFullDownPosition();
                    OnFullDownPositionUpdatedEvent?.Invoke();
                    OnMovedEvent?.Invoke(0.15f);
                    return true;
                }
            }
            return false;
        }

        public bool RightMove()
        {
            if (_data.IsLanded == false)
            {
                if (_mover.Move(Vector2Int.right))
                {
                    _downPositionCalculator.RecalculateFullDownPosition();
                    OnFullDownPositionUpdatedEvent?.Invoke();
                    OnMovedEvent?.Invoke(0.15f);
                    return true;
                }
            }
            return false;
        }

        public bool Rotate()
        {
            if (_data.IsLanded == false)
            {
                if (_rotator.Rotate())
                {
                    _downPositionCalculator.RecalculateFullDownPosition();
                    OnFullDownPositionUpdatedEvent?.Invoke();
                    OnMovedEvent?.Invoke(0.15f);
                    return true;
                }
            }

            return false;
        }
    }
}