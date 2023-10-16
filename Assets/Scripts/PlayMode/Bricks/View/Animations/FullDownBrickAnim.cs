using DG.Tweening;
using PlayMode.Map;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace PlayMode.Bricks
{
    public class FullDownBrickAnim : IBrickAnimation
    {
        public event Action OnAnimationEnded;

        public bool IsPlaying { get; private set; } = false;

        private ObjectPool<VisualEffect> _vfxPool;
        private CoordinateConverter _converter;
        private IReadOnlyList<IReadonlyBrickPart> _shape;
        private List<BlockObject> _blocks;
        private Vector3[] _targetPositions;
        private AudioSource _audioSource;
        private float _animationTime;
        private int _counter = 0;

        public FullDownBrickAnim(CoordinateConverter converter, IReadOnlyList<IReadonlyBrickPart> shape,
            List<BlockObject> blocks, float animationTime, ObjectPool<VisualEffect> visualEffect, AudioSource audioSource)
        {
            _converter = converter;
            _shape = shape;
            _blocks = blocks;
            _animationTime = animationTime;
            _vfxPool = visualEffect;

            _targetPositions = new Vector3[_shape.Count];
            for (int i = 0; i < _shape.Count; i++)
            {
                _targetPositions[i] = _converter.MapCoordinatesToWorld(_shape[i].Coordinates);
            }
            _audioSource = audioSource;
        }

        public void Animate()
        {
            if (IsPlaying == false)
            {
                IsPlaying = true;
                for (int i = 0; i < _shape.Count; i++)
                {
                    _blocks[i].transform.DOMove(_targetPositions[i], _animationTime)
                        .OnComplete(IncrementCompleteCounter);
                }
            }
        }

        private void IncrementCompleteCounter()
        {
            _counter++;

            if (_counter == _shape.Count)
            {
                _vfxPool.HideAllElements();
                foreach(var block in _shape)
                {
                    var effect = _vfxPool.GetElement();
                    effect.transform.position = _converter.MapCoordinatesToWorld(block.Coordinates) - new Vector3(0, 0.5f, 0.5f);
                    effect.Play();
                }
                OnAnimationEnded?.Invoke();
                IsPlaying = false;
            }
        }

        public void PlaySound()
        {
            _audioSource.Play();
        }
    }
}