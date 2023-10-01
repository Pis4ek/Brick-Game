using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace PlayMode.Map
{
    public class BlockMapLinesFallingChecker
    {
        private ReactiveDictionary<int, BlockLine> _gameMap;
        private Vector2Int _mapSize;

        public BlockMapLinesFallingChecker(ReactiveDictionary<int, BlockLine> gameMap, Vector2Int mapSize)
        {
            _gameMap = gameMap;
            _mapSize = mapSize;
        }

        public void CheckLinesFalling()
        {
            Queue<int> clearLines = new Queue<int>();
            for (int i = _mapSize.y - 1; i > 0; i--)
            {
                if (_gameMap.ContainsKey(i) == false)
                {
                    clearLines.Enqueue(i);
                }
                else if (clearLines.Count > 0)
                {
                    var newIndex = clearLines.Dequeue();
                    var line = _gameMap[i];
                    _gameMap.Remove(i);
                    line.SetNewHeight(newIndex);
                    _gameMap.Add(newIndex, line);
                    clearLines.Enqueue(i);
                }
            }
        }
    }
}
