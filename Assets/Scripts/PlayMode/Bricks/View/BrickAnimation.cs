using System;
using System.Collections;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickAnimation
    {
        public event Action OnAnimationEndedEvent;

        private CoordinateConverter _converter;
        private BrickData _data;
        private Vector3[] _targetPositions;
        private float _animationTime;
        private float _animationStepTime;

        public BrickAnimation(CoordinateConverter converter, BrickData data, float animationTime)
        {
            _converter = converter;
            _data = data;
            _animationTime = animationTime;
            _animationStepTime = _animationTime / 10;

            _targetPositions = new Vector3[_data.Shape.Count];
            for (int i = 0; i < _data.Shape.Count; i++)
            {
                _targetPositions[i] = _converter.MapCoordinatesToWorld(_data.Shape[i].Coordinates);
            }
        }

        public IEnumerator Animate()
        {
            for (float t = 0f; t <= 1; t += 0.1f)
            {
                for (int i = 0; i < _data.Shape.Count; i++)
                {
                    _data.Shape[i].GameObject.transform.position = Vector3.Lerp(
                        _data.Shape[i].GameObject.transform.position, _targetPositions[i], t);
                }
                yield return new WaitForSeconds(_animationStepTime);
            }

            OnAnimationEndedEvent?.Invoke();
        }
    }
}