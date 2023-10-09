using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class DefaultBrickAnim : IBrickAnimation
    {
        public event Action OnAnimationEndedEvent;

        private CoordinateConverter _converter;
        private IReadOnlyList<IReadonlyBrickPart> _shape;
        private Vector3[] _targetPositions;
        private float _animationTime;
        private int _counter = 0;

        public DefaultBrickAnim(CoordinateConverter converter, IReadOnlyList<IReadonlyBrickPart> shape, float animationTime)
        {
            _converter = converter;
            _shape = shape;
            _animationTime = animationTime;

            _targetPositions = new Vector3[_shape.Count];
            for (int i = 0; i < _shape.Count; i++)
            {
                _targetPositions[i] = _converter.MapCoordinatesToWorld(_shape[i].Coordinates);
            }
        }

        public void Animate()
        {
            for (int i = 0; i < _shape.Count; i++)
            {
                _shape[i].GameObject.transform.DOMove(_targetPositions[i], _animationTime)
                    .OnComplete(IncrementCompleteCounter);
            }
        }

        private void IncrementCompleteCounter()
        {
            _counter++;

            if (_counter == _shape.Count)
            {
                OnAnimationEndedEvent?.Invoke();
            }
        }
    }
}
