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
        private Queue<BrickAnimation> _animationQueue;
        private ObjectPool<Transform> _blockPool;
        private ObjectPool<BlockView> _transparentBlocksPool;
        private bool _isAnimating = false;

        public BrickView Init(Brick brick, CoordinateConverter converter, BrickData data)
        {
            var blockPrefab = Resources.Load<GameObject>("BlockObject");
            _blockPool = new ObjectPool<Transform>(blockPrefab.transform, 16, transform, "Block");
            var transparentblockPrefab = Resources.Load<GameObject>("TransparentBlockObject").GetComponent<BlockView>();
            _transparentBlocksPool = new ObjectPool<BlockView>(transparentblockPrefab, 16, transform, "Transparent_block");

            _animationQueue = new Queue<BrickAnimation>();
            _brick = brick;
            _converter = converter;
            _data = data;

            _brick.OnMovedEvent += MoveView;
            _brick.OnResetedEvent += ResetView;
            _brick.OnFullDownPositionUpdatedEvent += UpdateFallingView;

            return this;
        }

        private void FixedUpdate()
        {
            if(_isAnimating == false && _animationQueue.Count > 0)
            {
                _isAnimating = true;
                var animation = _animationQueue.Dequeue();
                StartCoroutine(animation.Animate());

                animation.OnAnimationEndedEvent += () =>
                { 
                    _isAnimating = false;
                    animation = null;
                };
            }
            else if (_data.IsLanded && _isAnimating == false && _animationQueue.Count == 0)
            {
                _data.SendBrickLandedEvent();
            }

        }

        private void MoveView(float time)
        {
            time = time * (1 - _animationQueue.Count * 0.1f);

            _animationQueue.Enqueue(new BrickAnimation(_converter, _data, time));
        }

        private void ResetView()
        {
            StopAllCoroutines();
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