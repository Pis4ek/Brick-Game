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
        private List<BrickPart> _resultShape;
        private SideBrickRotationOffsetCalculator _sideOffset;

        public BrickRotator(BrickData data, BlockMap map)
        {
            _data = data;
            _map = map;
            _sideOffset = new SideBrickRotationOffsetCalculator();
        }

        public bool Rotate()
        {
            _resultShape = new List<BrickPart>(_data.Shape.Count);
            _sideOffset.Reset();

            MakeShape();
            ShiftShape();

            if(CheckShape())
            {
                ApplyShape();
                return true;
            }
            return false;
        }

        private void MakeShape()
        {
            foreach (var block in _data.Shape)
            {
                var newBlock = new BrickPart();
                var xOffset = -(block.LocalCoordinates.x + block.LocalCoordinates.y);
                var yOffset = block.LocalCoordinates.x - block.LocalCoordinates.y;

                newBlock.Coordinates = block.Coordinates + new Vector2Int(xOffset, yOffset);
                newBlock.LocalCoordinates = block.LocalCoordinates + new Vector2Int(xOffset, yOffset);

                if (_map.HasBlockInPosition(newBlock.Coordinates))
                {
                    _sideOffset.ConsiderBlock(newBlock.LocalCoordinates.x);
                }

                _resultShape.Add(newBlock);
            }
        }

        private void ShiftShape()
        {
            foreach (var block in _resultShape)
            {
                block.Coordinates = block.Coordinates + new Vector2Int(_sideOffset.Offset, 0);
            }
        }

        private bool CheckShape()
        {
            foreach (var block in _resultShape)
            {
                if (_map.HasBlockInPosition(block.Coordinates))
                {
                    return false;
                }
            }
            return true;
        }

        private void ApplyShape()
        {
            for (int i = 0; i < _data.Shape.Count; i++)
            {
                _data.Shape[i].Coordinates = _resultShape[i].Coordinates;
                _data.Shape[i].LocalCoordinates = _resultShape[i].LocalCoordinates;
            }
        }
    }
}
