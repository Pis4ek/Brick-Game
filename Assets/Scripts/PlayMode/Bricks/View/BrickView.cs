using PlayMode.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickView : MonoBehaviour
    {
        private Brick _brick;
        private CoordinateConverter _converter;
        private BrickData _data;
        private Queue<BrickAnimationDoTween> _animationQueue;
        private ObjectPool<Transform> _blockPool;
        private ObjectPool<BlockView> _transparentBlocksPool;
        private bool _isAnimating = false;

        public BrickView Init(Brick brick, CoordinateConverter converter, BrickData data)
        {
            var blockPrefab = Resources.Load<GameObject>("BlockObject");
            _blockPool = new ObjectPool<Transform>(blockPrefab.transform, 16, transform, "Block");
            var transparentblockPrefab = Resources.Load<GameObject>("TransparentBlockObject").GetComponent<BlockView>();
            _transparentBlocksPool = new ObjectPool<BlockView>(transparentblockPrefab, 16, transform, "Transparent_block");

            _animationQueue = new Queue<BrickAnimationDoTween>();
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

        private void MoveView(float time)
        {
            time = time * (1 - _animationQueue.Count * 0.15f);
            _animationQueue.Enqueue(new BrickAnimationDoTween(_converter, _data, time));

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