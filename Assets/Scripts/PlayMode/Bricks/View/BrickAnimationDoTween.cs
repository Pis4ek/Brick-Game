using DG.Tweening;
using System;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickAnimationDoTween
    {
        public event Action OnAnimationEndedEvent;

        private CoordinateConverter _converter;
        private BrickData _data;
        private Vector3[] _targetPositions;
        private float _animationTime;
        private int _counter = 0;

        public BrickAnimationDoTween(CoordinateConverter converter, BrickData data, float animationTime)
        {
            _converter = converter;
            _data = data;
            _animationTime = animationTime;

            _targetPositions = new Vector3[_data.Shape.Count];
            for (int i = 0; i < _data.Shape.Count; i++)
            {
                _targetPositions[i] = _converter.MapCoordinatesToWorld(_data.Shape[i].Coordinates);
            }
        }

        public void Animate()
        {
            for (int i = 0; i < _data.Shape.Count; i++)
            {
                _data.Shape[i].GameObject.transform.DOMove(_targetPositions[i], _animationTime)
                    .OnComplete(IncrementCompleteCounter);
            }
        }

        private void IncrementCompleteCounter()
        {
            _counter++;

            if (_counter == _data.Shape.Count)
            {
                OnAnimationEndedEvent?.Invoke();
            }
        }
    }
}
