using PlayMode.Map;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickMover
    {
        private BrickShape _shape;
        private BlockMap _map;

        public BrickMover(BrickShape shape, BlockMap map)
        {
            _shape = shape;
            _map = map;
        }

        public bool Move(Vector2Int direction)
        {
            List<BrickPart> newShape = new List<BrickPart>(_shape.Blocks.Count);
            foreach (var block in _shape.Blocks)
            {
                var dX = block.Coordinates.x + direction.x;
                var dY = block.Coordinates.y - direction.y;

                var newBlock = new BrickPart();

                newBlock.Coordinates = new Vector2Int(dX, dY);

                if(_map.HasBlockInPosition(newBlock.Coordinates))
                {
                    return false;
                }
                newShape.Add(newBlock);
            }
            ApplyShape(newShape);

            return true;
        }

        private void ApplyShape(List<BrickPart> newShape)
        {
            for (int i = 0; i < _shape.Blocks.Count; i++)
            {
                _shape.Blocks[i].Coordinates = newShape[i].Coordinates;
            }
        }
    }
}