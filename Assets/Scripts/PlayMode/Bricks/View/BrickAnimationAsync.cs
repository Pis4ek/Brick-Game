using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickAnimationAsync
    {
        public event Action OnAnimationEndedEvent;

        private CoordinateConverter _converter;
        private BrickData _data;
        private Vector3[] _targetPositions;
        private int _animationTime;
        private int _animationStepTime;

        public BrickAnimationAsync(CoordinateConverter converter, BrickData data, float animationTime)
        {
            _converter = converter;
            _data = data;
            _animationTime = (int)(animationTime * 1000);
            _animationStepTime = _animationTime / 10;

            _targetPositions = new Vector3[_data.Shape.Count];
            for (int i = 0; i < _data.Shape.Count; i++)
            {
                _targetPositions[i] = _converter.MapCoordinatesToWorld(_data.Shape[i].Coordinates);
            }
        }

        public async void Animate()
        {
            for (float t = 0f; t <= 1; t += 0.1f)
            {
                for (int i = 0; i < _data.Shape.Count; i++)
                {
                    _data.Shape[i].GameObject.transform.position = Vector3.Lerp(
                        _data.Shape[i].GameObject.transform.position, _targetPositions[i], t);
                }
                await Task.Delay(15);
            }

            OnAnimationEndedEvent?.Invoke();
        }
    }
}