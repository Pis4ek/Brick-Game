using System;

namespace PlayMode.Bricks
{
    public interface IBrickAnimation
    {
        public event Action OnAnimationEndedEvent;
        public void Animate();
    }
}
