using PlayMode.Bricks;
using System;
using UnityEngine;
using UniRx;

namespace PlayMode.Map
{
    public class BlockLine
    {
        public event Action<int> OnFulledEvent;

        public ReactiveCollection<Block> LineRx;

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

        private Block[] Line;
        private CoordinateConverter _converter;
        private int _count;

        public BlockLine(int height, CoordinateConverter converter)
        {
            LineRx = new ReactiveCollection<Block>();
            Line = new Block[10];
            _converter = converter;
            Height = height;

        }

        public void Destroy()
        {
            for(int i = 0; i < Line.Length; i++)
            {
                Line[i].Destroy();
                Line[i] = null;
                Count--;
            }
        }

        public void AddBlock(IReadonlyBrickPart block, BlockView view, BlockMapData blockMapData)
        {
            Line[block.Coordinates.x] = new Block(block.Coordinates);
            view.Init(Line[block.Coordinates.x], block, _converter, blockMapData);

            Count++;
            if(Count == Line.Length)
            {
                OnFulledEvent?.Invoke(Height);
                Destroy();
            }
        }

        public bool HasBlock(int index)
        {
            return Line[index] != null;
        }

        public void SetNewHeight(int height)
        {
            foreach (var block in Line)
            {
                if(block != null)
                {
                    block.Coordinates = new Vector2Int(block.Coordinates.x, height);
                }
            }
            Height = height;
        }
    }
}
