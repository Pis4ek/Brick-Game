using PlayMode.Map;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickReseter
    {
        private BrickShape _shape;
        private BlockMap _map;

        public BrickReseter(BrickShape shape, BlockMap map)
        {
            _shape = shape;
            _map = map;
        }

        public bool Reset(Vector2Int startCoordiantes, BrickConfig config)
        {
            var localCenter = config.LocalCenter;
            var globalCenter = startCoordiantes + config.LocalCenter;
            var shape = new List<BrickPart>(16);

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (config.shape[y, x])
                    {
                        var block = new BrickPart();
                        block.LocalCoordinates = new Vector2Int(x - localCenter.x, y - localCenter.y);
                        block.Coordinates = globalCenter - new Vector2Int(localCenter.x - x, localCenter.y - y);

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
            _shape.Blocks = new List<BrickPart>(newShape.Count);

            for (int i = 0; i < newShape.Count; i++)
            {
                _shape.Blocks.Add(newShape[i]);
            }
        }
    }
}