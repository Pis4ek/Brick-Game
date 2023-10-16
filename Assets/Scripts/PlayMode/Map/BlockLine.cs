using PlayMode.Bricks;
using System;
using UnityEngine;
using UniRx;
using System.Linq;

namespace PlayMode.Map
{
    public class BlockLine
    {
        public event Action<int> OnFulledEvent;

        public ReactiveDictionary<int, Block> Blocks { get; private set; }
        public int Count => Blocks.Count;
        public int Height { get; private set; }

        public BlockLine(int height)
        {
            Blocks = new ReactiveDictionary<int, Block>();
            Height = height;
        }

        public void Destroy()
        {
            var keys = Blocks.Keys.ToArray();
            foreach (var key in keys)
            {
                Blocks[key].Destroy();
                Blocks.Remove(key);
            }

            Blocks.Dispose();
            Blocks.Clear();
        }

        public void AddBlock(IReadonlyBrickPart blockInfo, Color color)
        {
            var block = new Block(blockInfo.Coordinates, color);
            Blocks.Add(block.Coordinates.Value.x, block);

            if (Blocks.Count == 10)
            {
                Destroy();
                OnFulledEvent?.Invoke(Height);

            }
        }

        public bool HasBlock(int index)
        {
            if (Blocks.ContainsKey(index))
            {
                return true;
            }
            return false;

        }

        public void SetNewHeight(int height)
        {
            foreach (var block in Blocks)
            {
                block.Value.Coordinates.Value = new Vector2Int(block.Value.Coordinates.Value.x, height);
            }
            Height = height;
        }
    }
}
