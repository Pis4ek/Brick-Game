using System;

namespace PlayMode.Bricks
{
    public interface IBrickAnimation
    {
        public event Action OnAnimationEnded;
        public bool IsPlaying { get; }
        public void Animate();
        public void PlaySound();
    }
}
