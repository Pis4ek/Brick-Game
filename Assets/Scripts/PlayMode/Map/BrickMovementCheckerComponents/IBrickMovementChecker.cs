using PlayMode.Bricks;
using UnityEngine;

namespace PlayMode.Map
{
    public interface IBrickMovementChecker
    {
        public bool Check(Brick brick, Vector2Int direction);
    }
}
