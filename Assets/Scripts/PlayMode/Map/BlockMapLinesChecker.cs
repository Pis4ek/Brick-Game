using System;
using System.Collections.Generic;

namespace PlayMode.Map
{
    public class BlockMapLinesChecker
    {
        private BlockLine[] _gameMap;
        private CoordinateConverter _converter;

        public BlockMapLinesChecker(BlockLine[] gameMap, CoordinateConverter converter)
        {
            _gameMap = gameMap;
            _converter = converter;
        }

        public void CheckLines()
        {
            Queue<int> clearLines = new Queue<int>();
            for (int i = _gameMap.Length - 1; i > 0; i--)
            {
                if (_gameMap[i] == null)
                {
                    clearLines.Enqueue(i);
                }
                else if (clearLines.Count > 0)
                {
                    var newIndex = clearLines.Dequeue();
                    _gameMap[i].SetNewHeight(newIndex);
                    _gameMap[newIndex] = _gameMap[i];
                    _gameMap[i] = null;
                    clearLines.Enqueue(i);
                }
            }
        }
    }
}
