using System.Collections.Generic;
using UniRx;
using UnityEngine.VFX;

namespace PlayMode.Map
{
    public class BlockLineView
    {
        public Dictionary<Block, BlockView> Blocks = new Dictionary<Block, BlockView>();

        private ObjectPool<BlockObject> _viewPool;
        private ObjectPool<VisualEffect> _vfxPool;
        private CoordinateConverter _converter;
        private BlockLine _line;

        public BlockLineView(BlockLine line, ObjectPool<BlockObject> viewPool, ObjectPool<VisualEffect> vfxPool, CoordinateConverter converter)
        {
            _line = line;
            _viewPool = viewPool;
            _vfxPool = vfxPool;
            _converter = converter;

            _line.Blocks.ObserveAdd().Subscribe(block => { Add(block.Value); });

            _line.OnFulledEvent += Destroy;
        }

        private void Add(Block block)
        {
            var blockView = new BlockView(_converter, _viewPool.GetElement(), block);
            Blocks.Add(block, blockView);
        }

        private void Destroy(int height)
        {
            foreach(var block in Blocks.Keys)
            {
                ShowEffectForBlock(block);
            }
        }

        private void ShowEffectForBlock(Block block)
        {
            var effect = _vfxPool.GetElement();
            effect.transform.position = _converter.MapCoordinatesToWorld(block.Coordinates.Value) - new UnityEngine.Vector3(0, 0, 1);
            effect.Play();
        }
    }
}