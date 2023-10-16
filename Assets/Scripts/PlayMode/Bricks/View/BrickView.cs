using PlayMode.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.VFX;

namespace PlayMode.Bricks
{
    public class BrickView : MonoBehaviour
    {
        [SerializeField] AudioSource _strongThudAudio;
        [SerializeField] AudioSource _thudAudio;

        private BrickAnimationQueue _queue = new BrickAnimationQueue();
        private List<BlockObject> _blocks = new List<BlockObject>();
        private ObjectPool<BlockObject> _blockPool;
        private ObjectPool<VisualEffect> _vfxPool;
        private CoordinateConverter _converter;
        private Brick _brick;

        public BrickView Init(Brick brick, CoordinateConverter converter)
        {
            CreatePools();
            _brick = brick;
            _converter = converter;

            _brick.OnMovedEvent += MoveView;
            _brick.OnResetedEvent += ResetView;
            _brick.OnCanNotFall += () => {
                if(_queue.AnimsCount == 0)
                {
                    _queue.CurrentAnimation.PlaySound();
                    _brick.SendBrickLandedEvent();
                }

            };

            _queue.OnLastAnimEnded += () =>
            {
                if (_brick.IsLanded.Value)
                {
                    _queue.CurrentAnimation.PlaySound();
                    _brick.SendBrickLandedEvent();
                }
            };

            return this;
        }

        private async void CreatePools()
        {
            var blockPrefab = Resources.Load<GameObject>("BlockObject").GetComponent<BlockObject>();
            _blockPool = new ObjectPool<BlockObject>(blockPrefab, 16, transform, "Block");

            var handle = Addressables.LoadAssetAsync<GameObject>("BrickDownDustEffect");
            await handle.Task;
            var visualEffect = handle.Result.GetComponent<VisualEffect>();
            _vfxPool = new ObjectPool<VisualEffect>(visualEffect, 16, transform, "VFXPoolElement");
            Addressables.ReleaseInstance(handle);

        }

        private void MoveView(BrickAnimationType type)
        {
            var time = 0.15f * (1 - _queue.AnimsCount * 0.15f);
            IBrickAnimation animation;

            if (type == BrickAnimationType.FullDown)
            {
                animation = new FullDownBrickAnim(_converter, _brick.Shape.BlocksShape, 
                    _blocks, time, _vfxPool, _strongThudAudio);
            }
            else
            {
                animation = new DefaultBrickAnim(_converter, _brick.Shape.BlocksShape, 
                    _blocks, time, _thudAudio);
            }
            _queue.AddAnimation(animation);
        }

        private void ResetView()
        {
            _blockPool.HideAllElements();
            foreach (var block in _brick.Shape.BlocksShape)
            {
                var blockObj = _blockPool.GetElement();
                blockObj.gameObject.transform.position = _converter.MapCoordinatesToWorld(block.Coordinates);

                blockObj.Renderer.material.color = _brick.Shape.Color;
                _blocks.Add(blockObj);
            }
        }
    }
}