using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.VFX;

namespace PlayMode.Map
{
    public class BlockMapView
    {
        private Dictionary<BlockLine, BlockLineView> _lineViews;
        private ObjectPool<VisualEffect> _vfxPool;
        private ObjectPool<BlockObject> _objPool;
        private CoordinateConverter _converter;
        private BlockMap _blockMap;


        public BlockMapView(BlockMap blockMap, CoordinateConverter converter, Transform blockContainer, VisualEffect _destroingEffect)
        {
            _lineViews = new Dictionary<BlockLine, BlockLineView>();

            _blockMap = blockMap;
            _converter = converter;

            CreatePools(blockContainer, _destroingEffect);

            SubscribeAllEvents();
        }

        private void CreatePools(Transform blockContainer, VisualEffect _destroingEffect)
        {
            var prefab = Resources.Load<GameObject>("BlockObject").GetComponent<BlockObject>();
            _objPool = new ObjectPool<BlockObject>(prefab, 100, blockContainer, "BlockViewObject");
            _objPool.AutoExpand = true;

            _vfxPool = new ObjectPool<VisualEffect>(_destroingEffect, 50, blockContainer, "EffectObject");
            _vfxPool.AutoExpand = true;
        }
        private void SubscribeAllEvents()
        {
            _blockMap.Lines.ObserveAdd().Subscribe(line => 
            {
                _lineViews.Add(line.Value, new BlockLineView(line.Value, _objPool, _vfxPool, _converter));
            });

            _blockMap.Lines.ObserveRemove().Subscribe(line =>
            {
                _lineViews.Remove(line.Value);
            });

        }
    }
}