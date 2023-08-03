using UnityEngine;

namespace PlayMode.Bricks
{
    public interface IControllableBrick
    {
        public bool Move(Vector2Int direction);
        public bool Rotate();
    }
}
