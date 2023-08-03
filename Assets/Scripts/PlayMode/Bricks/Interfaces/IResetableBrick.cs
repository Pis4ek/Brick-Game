using UnityEngine;

namespace PlayMode.Bricks
{
    public interface IResetableBrick
    {
        public bool ResetBrick(Vector2Int startCoordiantes, BrickConfig brickConfig);
    }
}