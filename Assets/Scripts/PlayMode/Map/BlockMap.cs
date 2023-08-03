using PlayMode.Bricks;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Map
{
    public class BlockMap : IService, IBlockMapActions
    {
        public event Action OnBrickLandedEvent;
        public event Action<int> OnBlocksAddedEvent;
        public event Action<int> OnLinesDestroyedEvent;

        public BlockLine[] GameMap { get; private set; }
        public Vector2Int MapSize { get; private set; } = new Vector2Int(10, 15);
        public Vector3 WorldStartMap { get; private set; } = new Vector3(-4.5f, 6.5f, -0.5f);
        public float CellSize { get; private set; } = 1f;

        private ObjectPool<BlockView> _objectPool;
        private int _destroyedLinesCount = 0;
        private CoordinateConverter _converter;
        private BlockMapLinesChecker _linesChecker;

        public BlockMap(Transform blockContainer)
        {
            GameMap = new BlockLine[MapSize.y];

            var prefab = Resources.Load<GameObject>("BlockObject").AddComponent<BlockView>();
            _objectPool = new ObjectPool<BlockView>(prefab, 50, blockContainer);
            _objectPool.AutoExpand = true;

            _converter = new CoordinateConverter(CellSize, WorldStartMap);
            _linesChecker = new BlockMapLinesChecker(GameMap, _converter);
        }

        public bool HasBlockInPosition(int X, int Y)
        {
            try
            {
                if (X >= MapSize.x || X < 0)
                    return true;
                if (GameMap[Y] == null)
                    return false;
                return GameMap[Y].HasBlock(X);
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
                if(GameMap[block.Coordinates.y] == null)
                {
                    GameMap[block.Coordinates.y] = new BlockLine(block.Coordinates.y, _converter);
                    GameMap[block.Coordinates.y].OnFulledEvent += OnLineDestroyed;
                }
                GameMap[block.Coordinates.y].AddBlock(block, _objectPool.GetElement().gameObject, this);
            }
            OnBrickLandedEvent?.Invoke();
            OnBlocksAddedEvent?.Invoke(shape.Count);

            CheckDestroyedLines();
        }

        private void CheckDestroyedLines()
        {
            if (_destroyedLinesCount > 0)
            {
                OnLinesDestroyedEvent?.Invoke(_destroyedLinesCount);
                //RecalculateFalling();
                _linesChecker.CheckLines();
            }
            _destroyedLinesCount = 0;
        }

        private void RecalculateFalling()
        {
            Queue<int> clearLines = new Queue<int>();
            for (int i = GameMap.Length - 1; i > 0; i--)
            {
                if (GameMap[i] == null)
                {
                    clearLines.Enqueue(i);
                }
                else if (clearLines.Count > 0)
                {
                    var newIndex = clearLines.Dequeue();
                    GameMap[i].SetNewHeight(newIndex, _converter.MapHeightToWorld(newIndex));
                    GameMap[newIndex] = GameMap[i];
                    GameMap[i] = null;
                    clearLines.Enqueue(i);
                }
            }
        }

        private void OnLineDestroyed(int height)
        {
            _destroyedLinesCount++;
        }
    }
}
