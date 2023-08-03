using System;
using System.Collections.Generic;

namespace PlayMode.Map
{
    public class BlockMapLinesChecker
    {
        private BlockLine[] _gameMap;
        private CoordinateConverter _converter;
        Queue<int> _combineLines;

        public BlockMapLinesChecker(BlockLine[] gameMap, CoordinateConverter converter)
        {
            _gameMap = gameMap;
            _converter = converter;
        }

        public void CheckLines()
        {
           _combineLines = new Queue<int>();

            for (int i = _gameMap.Length - 1; i > 0; i--)
            {
                if (_gameMap[i] == null)
                {
                    _combineLines.Enqueue(i);
                }
                else if (_gameMap[i].IsFull)
                {
                    _gameMap[i].Damage();
                    if (_gameMap[i].IsClear)
                    {
                        _gameMap[i] = null;
                        _combineLines.Enqueue(i);
                    }
                    else
                    {
                        DamagedFalling(i);
                    }
                }
                else
                {
                    Falling(i);
                }
            }
        }

        private void DamagedFalling(int index)
        {
            if (_combineLines.Count > 0)
            {
                var newIndex = _combineLines.Peek();
                if(_gameMap[newIndex] == null)
                {
                    Falling(index);
                    return;
                }
                else
                {
                    if (_gameMap[newIndex].TryCombine(_gameMap[index]))
                    {
                        if (_gameMap[index].IsClear) _gameMap[index] = null;
                        
                    }
                }
            }
            _combineLines.Enqueue(index);
        }

        private void Falling(int index)
        {
            if (_combineLines.Count > 0)
            {
                var newIndex = _combineLines.Dequeue();
                _gameMap[index].SetNewHeight(newIndex, _converter.MapHeightToWorld(newIndex));
                _gameMap[newIndex] = _gameMap[index];
                _gameMap[index] = null;
                _combineLines.Enqueue(index);
            }
        }
    }
}
