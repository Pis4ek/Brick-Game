using PlayMode.Map;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class DownPositionCalculator
    {
        private BrickData _data;
        private BlockMap _map;

        public DownPositionCalculator(BrickData data, BlockMap map)
        {
            _data = data;
            _map = map;
        }


        public void RecalculateFullDownPosition()
        {
            _data.FullDownPosition = new List<Vector2Int>(_data.Shape.Count);
            foreach (var block in _data.Shape)
            {
                _data.FullDownPosition.Add(new Vector2Int(block.Coordinates.x, block.Coordinates.y));
            }
            while (true)
            {
                var positionList = new List<Vector2Int>(_data.Shape.Count);
                foreach (var block in _data.FullDownPosition)
                {
                    var dY = block.y + 1;
                    if (_map.HasBlockInPosition(new Vector2Int(block.x, dY)))
                    {
                        return;
                    }
                    positionList.Add(new Vector2Int(block.x, dY));
                }
                _data.FullDownPosition = positionList;
            }
        }

        public void FallToDown()
        {
            for (int i = 0; i < _data.Shape.Count; i++)
            {
                _data.Shape[i].Coordinates = _data.FullDownPosition[i];
            }
        }
    }
}
