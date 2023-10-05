﻿using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.VFX;

namespace PlayMode.Bricks
{
    public class FullDownBrickAnim : IBrickAnimation
    {
        public event Action OnAnimationEndedEvent;

        private ObjectPool<VisualEffect> _vfxPool;
        private CoordinateConverter _converter;
        private BrickData _data;
        private Vector3[] _targetPositions;
        private float _animationTime;
        private int _counter = 0;

        public FullDownBrickAnim(CoordinateConverter converter, BrickData data, float animationTime, ObjectPool<VisualEffect> visualEffect)
        {
            _converter = converter;
            _data = data;
            _animationTime = animationTime;
            _vfxPool = visualEffect;

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
                _vfxPool.HideAllElements();
                foreach(var block in _data.Shape)
                {
                    var effect = _vfxPool.GetElement();
                    effect.transform.position = _converter.MapCoordinatesToWorld(block.Coordinates) - new Vector3(0, 0.5f, 0.5f);
                    effect.Play();
                }
                OnAnimationEndedEvent?.Invoke();
            }
        }
    }
}