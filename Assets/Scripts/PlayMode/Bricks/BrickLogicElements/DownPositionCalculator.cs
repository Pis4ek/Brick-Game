using PlayMode.Map;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class DownPositionCalculator
    {
        private BrickShape _shape;
        private BlockMap _map;
        private List<Vector2Int> _fullDownPosition;

        public DownPositionCalculator(BrickShape shape, BlockMap map)
        {
            _shape = shape;
            _map = map;
        }


        public List<Vector2Int> RecalculateFullDownPosition()
        {
            _fullDownPosition = new List<Vector2Int>(_shape.Blocks.Count);
            foreach (var block in _shape.Blocks)
            {
                _fullDownPosition.Add(new Vector2Int(block.Coordinates.x, block.Coordinates.y));
            }
            //while (true)
            for(int i = 0; i < 30; i++)
            {
                var positionList = new List<Vector2Int>(_shape.Blocks.Count);
                foreach (var block in _fullDownPosition)
                {
                    var dY = block.y + 1;
                    if (_map.HasBlockInPosition(new Vector2Int(block.x, dY)))
                    {
                        return _fullDownPosition;
                    }
                    positionList.Add(new Vector2Int(block.x, dY));
                }
                _fullDownPosition = positionList;
            }
            Debug.Log("I am cringovich");
            return null;
        }

        public void FallToDown()
        {
            for (int i = 0; i < _shape.Blocks.Count; i++)
            {
                _shape.Blocks[i].Coordinates = _fullDownPosition[i];
            }
        }
    }
}
