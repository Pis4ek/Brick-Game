using DG.Tweening;
using PlayMode.Map;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class DefaultBrickAnim : IBrickAnimation
    {
        public event Action OnAnimationEnded;

        public bool IsPlaying { get; private set; } = false;

        private CoordinateConverter _converter;
        private IReadOnlyList<IReadonlyBrickPart> _shape;
        private List<BlockObject> _blocks;
        private Vector3[] _targetPositions;
        private AudioSource _audioSource;
        private float _animationTime;
        private int _counter = 0;

        public DefaultBrickAnim(CoordinateConverter converter, IReadOnlyList<IReadonlyBrickPart> shape,
            List<BlockObject> blocks, float animationTime, AudioSource audioSource)
        {
            _converter = converter;
            _shape = shape;
            _blocks = blocks;
            _animationTime = animationTime;

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
