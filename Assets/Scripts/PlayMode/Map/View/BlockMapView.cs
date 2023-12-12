using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
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


        public BlockMapView(BlockMap blockMap, CoordinateConverter converter, Transform blockContainer)
        {
            _lineViews = new Dictionary<BlockLine, BlockLineView>();

            _blockMap = blockMap;
            _converter = converter;

            CreatePools(blockContainer);

            SubscribeAllEvents();
        }

        private async void CreatePools(Transform blockContainer)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("BlockObject");
            await handle.Task;
            var prefab = handle.Result.GetComponent<BlockObject>();

            
            _objPool = new ObjectPool<BlockObject>(prefab, 100, blockContainer, "BlockViewObject");
            _objPool.AutoExpand = true;

            var destroyEffectHandle = Addressables.LoadAssetAsync<GameObject>("BlockDestroingEffect");
            await destroyEffectHandle.Task;
            var destroyEffect = destroyEffectHandle.Result.GetComponent<VisualEffect>();

            _vfxPool = new ObjectPool<VisualEffect>(destroyEffect, 50, blockContainer, "EffectObject");
            _vfxPool.AutoExpand = true;

            Addressables.ReleaseInstance(destroyEffectHandle);
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