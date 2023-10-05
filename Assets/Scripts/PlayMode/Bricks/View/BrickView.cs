using PlayMode.Map;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.VFX;

namespace PlayMode.Bricks
{
    public class BrickView : MonoBehaviour
    {
        private Brick _brick;
        private CoordinateConverter _converter;
        private BrickData _data;
        private Queue<IBrickAnimation> _animationQueue;
        private ObjectPool<Transform> _blockPool;
        private ObjectPool<BlockObject> _transparentBlocksPool;
        private ObjectPool<VisualEffect> _vfxPool;
        private bool _isAnimating = false;

        public BrickView Init(Brick brick, CoordinateConverter converter, BrickData data, VisualEffect visualEffect)
        {
            CreatePools();
            _animationQueue = new Queue<IBrickAnimation>();
            _brick = brick;
            _converter = converter;
            _data = data;

            _brick.OnMovedEvent += MoveView;
            _brick.OnResetedEvent += ResetView;
            _brick.OnFullDownPositionUpdatedEvent += UpdateFallingView;
            _brick.OnCanNotFall += () => {
                if (_data.IsLanded && _isAnimating == false && _animationQueue.Count == 0)
                {
                    _data.SendBrickLandedEvent();
                }
            };

            return this;
        }

        private async void CreatePools()
        {
            var blockPrefab = Resources.Load<GameObject>("BlockObject").GetComponent<BlockObject>();
            _blockPool = new ObjectPool<Transform>(blockPrefab.transform, 16, transform, "Block");

            var transparentblockPrefab = Resources.Load<GameObject>("TransparentBlockObject").GetComponent<BlockObject>();
            _transparentBlocksPool = new ObjectPool<BlockObject>(transparentblockPrefab, 16, transform, "Transparent_block");

            var handle = Addressables.LoadAssetAsync<GameObject>("BrickDownDustEffect");
            await handle.Task;
            var visualEffect = handle.Result.GetComponent<VisualEffect>();
            _vfxPool = new ObjectPool<VisualEffect>(visualEffect, 16, transform, "VFXPoolElement");
            Addressables.ReleaseInstance(handle);

        }

        private void MoveView(BrickAnimationType type)
        {
            var time = 0.15f * (1 - _animationQueue.Count * 0.15f);

            if (type == BrickAnimationType.FullDown)
            {
                _animationQueue.Enqueue(new FullDownBrickAnim(_converter, _data, time, _vfxPool));
            }
            else
            {
                _animationQueue.Enqueue(new DefaultBrickAnim(_converter, _data, time));
            }

            CheckAnimations();
        }

        private void CheckAnimations()
        {
            if (_isAnimating == false && _animationQueue.Count > 0)
            {
                _isAnimating = true;
                var animation = _animationQueue.Dequeue();
                animation.Animate();

                animation.OnAnimationEndedEvent += () =>
                {
                    _isAnimating = false;
                    if (_animationQueue.Count > 0)
                    {
                        CheckAnimations();
                    }
                    if (_data.IsLanded && _isAnimating == false && _animationQueue.Count == 0)
                    {
                        _data.SendBrickLandedEvent();
                    }
                };
            }

        }

        private void ResetView()
        {
            _isAnimating = false;

            _blockPool.HideAllElements();
            foreach (var block in _data.Shape)
            {
                block.GameObject = _blockPool.GetElement().gameObject;
                block.GameObject.transform.position = _converter.MapCoordinatesToWorld(block.Coordinates);

                block.Renderer.material.color = _data.Color;
            }
        }

        private void UpdateFallingView()
        {
            _transparentBlocksPool.HideAllElements();
            for (int i = 0; i < _data.Shape.Count; i++)
            { 
                var obj = _transparentBlocksPool.GetElement();
                obj.transform.position = _converter.MapCoordinatesToWorld(_data.FullDownPosition[i]);
                obj.Renderer.material.color = new Color(_data.Color.r, _data.Color.g, _data.Color.b, 0.2f);
            }
        }
    }
}