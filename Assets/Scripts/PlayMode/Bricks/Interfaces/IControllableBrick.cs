using UnityEngine;

namespace PlayMode.Bricks
{
    public interface IControllableBrick
    {
        public bool DownMove();
        public bool FullDownMove();
        public bool LeftMove();
        public bool RightMove();
        public bool Rotate();
    }
}
