using PlayMode.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickRotator
    {
        private BrickData _data;
        private BlockMap _map;

        public BrickRotator(BrickData data, BlockMap map)
        {
            _data = data;
            _map = map;
        }

        public bool Rotate()
        {
            List<BrickPart> newShape = new List<BrickPart>(_data.Shape.Count);
            foreach (var block in _data.Shape)
            {
                var localXY = block.LocalCoordinates;

                var xOffset = -(localXY.x + localXY.y);
                var yOffset = localXY.x - localXY.y;

                var newBlock = new BrickPart();

                newBlock.Coordinates = block.Coordinates + new Vector2Int(xOffset, yOffset);
                newBlock.LocalCoordinates = block.LocalCoordinates + new Vector2Int(xOffset, yOffset);

                if(_map.HasBlockInPosition(newBlock.Coordinates.x, newBlock.Coordinates.y))
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
            for (int i = 0; i < _data.Shape.Count; i++)
            {
                _data.Shape[i].Coordinates = newShape[i].Coordinates;
                _data.Shape[i].LocalCoordinates = newShape[i].LocalCoordinates;
            }
        }
    }
}
