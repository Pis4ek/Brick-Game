using PlayMode.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickMover
    {
        private BrickData _data;
        private BlockMap _map;
        private CoordinateConverter _converter;

        public BrickMover(BrickData data, BlockMap map)
        {
            _data = data;
            _map = map;
            _converter = new CoordinateConverter(_map.CellSize, _map.WorldStartMap);
        }

        public bool Move(Vector2Int direction)
        {
            List<BrickPart> newShape = new List<BrickPart>(_data.Shape.Count);
            foreach (var block in _data.Shape)
            {
                var dX = block.Coordinates.x + direction.x;
                var dY = block.Coordinates.y - direction.y;

                var newBlock = new BrickPart();

                newBlock.Coordinates = new Vector2Int(dX, dY);

                if(_map.HasBlockInPosition(dX, dY))
                {
                    if (direction == Vector2Int.down)
                    {
                        _map.AddBrick(_data.Shape);
                    }
                    return false;
                }
                newShape.Add(newBlock);
            }

            ApplyShape(newShape);

            return true;
        }

        private void ApplyShape(List<BrickPart> newShape)
        {
            for (int i = 0; i < _data.Shape.Count; i++)
            {
                _data.Shape[i].Coordinates = newShape[i].Coordinates;
            }
        }
    }
}