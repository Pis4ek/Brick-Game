using PlayMode.Bricks;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Map
{
    public class BlockMap : IService, IBlockMapActions
    {
        public event Action<int> OnBlocksAddedEvent;
        public event Action<int> OnLinesDestroyedEvent;

        public BlockMapData Data => _data;

        private BlockMapData _data;
        private CoordinateConverter _converter;
        private BlockMapLinesChecker _linesChecker;

        public BlockMap(BlockMapData data)
        {
            _data = data;
            _converter = new CoordinateConverter(_data.CellSize, _data.WorldStartMap);
            _linesChecker = new BlockMapLinesChecker(_data.GameMap, _converter);
        }

        public bool HasBlockInPosition(int X, int Y)
        {
            try
            {
                if (X >= _data.MapSize.x || X < 0)
                    return true;
                if (_data.GameMap[Y] == null)
                    return false;
                return _data.GameMap[Y].HasBlock(X);
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        public void AddBrick(IReadOnlyList<IReadonlyBrickPart> shape)
        {
            foreach(var block in shape)
            {
                if(_data.GameMap[block.Coordinates.y] == null)
                {
                    _data.GameMap[block.Coordinates.y] = new BlockLine(block.Coordinates.y, _converter);
                    _data.GameMap[block.Coordinates.y].OnFulledEvent += OnLineDestroyed;
                }
                _data.GameMap[block.Coordinates.y].AddBlock(block, _data._objectPool.GetElement());
            }
            OnBlocksAddedEvent?.Invoke(shape.Count);

            CheckDestroyedLines();
        }

        private void CheckDestroyedLines()
        {
            if (_data._destroyedLinesCount > 0)
            {
                OnLinesDestroyedEvent?.Invoke(_data._destroyedLinesCount);
                _linesChecker.CheckLines();
            }
            _data._destroyedLinesCount = 0;
        }

        private void OnLineDestroyed(int height)
        {
            _data._destroyedLinesCount++;
            _data.GameMap[height] = null;
        }
    }
}
