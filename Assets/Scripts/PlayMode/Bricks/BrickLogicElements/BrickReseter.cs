using PlayMode.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickReseter
    {
        private BrickData _data;
        private BlockMap _map;

        public BrickReseter(BrickData data, BlockMap map)
        {
            _data = data;
            _map = map;
        }

        public bool Reset(Vector2Int startCoordiantes, BrickConfig config)
        {
            _data.IsLanded = false;
            _data.Color = config.Color;
            _data.LocalCenter = config.LocalCenter;
            _data.GlobalCenter = startCoordiantes + config.LocalCenter;
            var shape = new List<BrickPart>(16);

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (config.shape[y, x])
                    {
                        var block = new BrickPart();
                        block.LocalCoordinates = new Vector2Int(x - _data.LocalCenter.x, y - _data.LocalCenter.y);
                        block.Coordinates = _data.GlobalCenter - new Vector2Int(_data.LocalCenter.x - x, _data.LocalCenter.y - y);

                        if(_map.HasBlockInPosition(block.Coordinates))
                        {
                            return false;
                        }
                        shape.Add(block);
                    }
                }
            }

            ApplyShape(shape, config);

            return true;
        }

        private void ApplyShape(List<BrickPart> newShape, BrickConfig config)
        {
            _data.Shape = new List<BrickPart>(newShape.Count);

            for (int i = 0; i < newShape.Count; i++)
            {
                _data.Shape.Add(newShape[i]);
            }
        }
    }
}