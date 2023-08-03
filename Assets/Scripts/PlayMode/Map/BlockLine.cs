using PlayMode.Bricks;
using System;
using UnityEngine;

namespace PlayMode.Map
{
    public class BlockLine
    {
        public event Action<int> OnFulledEvent;
        public event Action OnTest;

        public int Count
        {
            get { return _count; }
            private set
            {
                _count = value;
                if (_count == 0) IsClear = true;
                else if (_count > 0) IsClear = false;
                if (_count == Line.Length) IsFull = true;
                else if (_count < Line.Length) IsFull = false;
            }
        }
        public bool IsFull { get; private set; } = false;
        public bool IsClear { get; private set; } = true;
        public int Height { get; private set; }

        private DefaultBlock[] Line;
        private CoordinateConverter _converter;
        private int _count;

        public BlockLine(int height, CoordinateConverter converter)
        {
            Line = new DefaultBlock[10];
            _converter = converter;
            Height = height;
        }

        public int Damage()
        {
            int destroyedBlockCount = 0;
            for(int i = 0; i < Line.Length; i++)
            {
                Line[i].HP--;
                if (Line[i].HP <= 0)
                {
                    Line[i].GameObject.SetActive(false);
                    Line[i] = null;
                    Count--;
                    destroyedBlockCount++;
                }
            }
            return destroyedBlockCount;
        }

        public void AddBlock(IReadonlyBrickPart block, GameObject gameObject, IBlockMapActions actions)
        {
            switch (block.Type)
            {
                case BlockType.Ice:
                    Line[block.Coordinates.x] = new IceBlock(block, gameObject);
                    break;
                case BlockType.Stone:
                    Line[block.Coordinates.x] = new StoneBlock(block, gameObject);
                    break;
                case BlockType.Lava:
                    Line[block.Coordinates.x] = new LavaBlock(block, gameObject, actions);
                    break;
                case BlockType.Obsidian:
                    Line[block.Coordinates.x] = new ObsidianBlock(block, gameObject);
                    break;
                default:
                    Line[block.Coordinates.x] = new DefaultBlock(block, gameObject);
                    break;
            }

            Count++;
            if(Count == Line.Length)
            {
                OnFulledEvent?.Invoke(Height);
            }
        }

        public bool HasBlock(int index)
        {
            return Line[index] != null;
        }

        public bool TryCombine(BlockLine combinedLine)
        {
            bool result = false;

            for (int i = 0; i < Line.Length; i++)
            {
                if (Line[i] == null && combinedLine.Line[i] != null)
                {
                    Line[i] = combinedLine.Line[i];
                    combinedLine.Line[i] = null;
                    combinedLine.Count--;

                    var position = Line[i].GameObject.transform.position;
                    Line[i].Coordinates = new Vector2Int(Line[i].Coordinates.x, Height);
                    Line[i].GameObject.transform.position = new Vector3(position.x, _converter.MapHeightToWorld(Height), position.z);
                    result = true;
                }
            }

            return result;
        }

        public void SetNewHeight(int newMapHeight, float newWorldHeight)
        {
            foreach (var block in Line)
            {
                if(block != null)
                {
                    var blockPosition = block.GameObject.transform.position;
                    block.Coordinates = new Vector2Int(block.Coordinates.x, newMapHeight);
                    block.GameObject.transform.position = new Vector3(blockPosition.x, newWorldHeight, blockPosition.z);
                }
            }
            Height = newMapHeight;
        }
    }
}
