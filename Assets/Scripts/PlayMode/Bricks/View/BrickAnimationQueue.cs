using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickAnimationQueue
    {
        public Action OnCurrentAnimStarted;
        public Action OnCurrentAnimEnded;
        public Action OnLastAnimEnded;

        public IBrickAnimation CurrentAnimation { get; private set; }
        public bool IsPlaying { get; private set; }
        public int AnimsCount => _animations.Count;

        private Queue<IBrickAnimation> _animations = new Queue<IBrickAnimation>();

        public bool TryStartPlaying()
        {
            if(_animations.Count > 0 && IsPlaying == false)
            {
                IsPlaying = true;
                CurrentAnimation = _animations.Dequeue();
                OnCurrentAnimStarted?.Invoke();
                CurrentAnimation.Animate();
                CurrentAnimation.OnAnimationEnded += OnEndAnimation;

                return true;
            }
            return false;
        }

        public void AddAnimation(IBrickAnimation animation)
        {
            _animations.Enqueue(animation);
            TryStartPlaying();
        }

        private void OnEndAnimation()
        {
            OnCurrentAnimEnded?.Invoke();

            if(_animations.Count == 0)
            {
                OnLastAnimEnded?.Invoke();
            }

            IsPlaying = false;
            TryStartPlaying();
        }
    }
}