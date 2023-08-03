using PlayMode.Map;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class Brick : IService, IResetableBrick, IControllableBrick
    {
        public event Action<Color> OnResetedEvent;
        public event Action OnMovedEvent;

        public IReadOnlyList<IReadonlyBrickPart> Shape => _data.Shape;

        private BrickRotator _rotator;
        private BrickMover _mover;
        private BrickReseter _reseter;
        private BlockMap _map;
        private BrickData _data;

        public Brick(BlockMap map, BrickData data)
        {
            _map = map;
            _data = data;
            _rotator = new BrickRotator(_data, map);
            _mover = new BrickMover(_data, map);
            _reseter = new BrickReseter(_data, map);
           
        }

        public bool ResetBrick(Vector2Int startCoordiantes, BrickConfig config)
        {
            if(_reseter.Reset(startCoordiantes, config))
            {
                OnResetedEvent?.Invoke(config.Color);
                return true;
            }
            return false;
        }

        public bool Move(Vector2Int direction)
        {
            if (_mover.Move(direction))
            {
                OnMovedEvent?.Invoke();
                return true;
            }
            return false;
        }

        public bool Rotate()
        {
            if (_rotator.Rotate())
            {
                OnMovedEvent?.Invoke();
                return true;
            }
            return false;
        }

        public string ShapeToString()
        {
            var message = "BRICK TEST_SHAPE:\n";
            foreach (var block in Shape)
            {
                message += $"(L({block.LocalCoordinates.x},{block.LocalCoordinates.y}) " +
                    $"G({block.Coordinates.x},{block.Coordinates.y})) \n";
            }
            return message;
        }
    }
}