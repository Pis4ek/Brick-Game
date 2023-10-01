using PlayMode.Bricks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace PlayMode.Map
{
    public class BlockMap : IBlockMapActions
    {
        public event Action<int> OnBlocksAddedEvent;
        public event Action<int> OnLinesDestroyedEvent;

        public ReactiveDictionary<int, BlockLine> Lines;

        public Vector2Int MapSize { get; private set; } = new Vector2Int(10, 15);
        public Vector3 WorldStartMap { get; private set; } = new Vector3(-4.5f, 6.5f, -0.5f);
        public float CellSize { get; private set; } = 1f;

        private int _destroyedLinesCount = 0;
        private BlockMapLinesFallingChecker _linesChecker;

        public BlockMap()
        {
            Lines = new ReactiveDictionary<int, BlockLine>();
            _linesChecker = new BlockMapLinesFallingChecker(Lines, MapSize);
        }

        public bool HasBlockInPosition(Vector2Int coordinates)
        {
            if (CheckCoordinateValidity(coordinates) == false)
                return true;
            if (Lines.ContainsKey(coordinates.y) && Lines[coordinates.y].HasBlock(coordinates.x))
                return true;

            return false;
        }

        public void AddBrick(IReadOnlyList<IReadonlyBrickPart> shape)
        {
            foreach(var block in shape)
            {
                var height = block.Coordinates.y;
                if (Lines.ContainsKey(height))
                {
                    Lines[height].AddBlock(block);
                }
                else
                {
                    var newLine = new BlockLine(block.Coordinates.y);
                    Lines.Add(height, newLine);
                    Lines[height].OnFulledEvent += OnLineDestroyed;
                    Lines[height].AddBlock(block);
                }
            }
            OnBlocksAddedEvent?.Invoke(shape.Count);

            if (_destroyedLinesCount > 0)
            {
                OnLinesDestroyedEvent?.Invoke(_destroyedLinesCount);
                _linesChecker.CheckLinesFalling();
            }
            _destroyedLinesCount = 0;
        }

        private void OnLineDestroyed(int height)
        {
            _destroyedLinesCount++;
            Lines.Remove(height);
        }

        private bool CheckCoordinateValidity(Vector2Int coordinates)
        {
            if (coordinates.x >= MapSize.x || coordinates.x < 0)
                return false;
            if (coordinates.y >= MapSize.y || coordinates.y < 0)
                return false;

            return true;
        }
    }
}
