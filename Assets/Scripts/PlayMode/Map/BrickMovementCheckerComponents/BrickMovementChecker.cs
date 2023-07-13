using PlayMode.Bricks;
using System;
using UnityEngine;

namespace PlayMode.Map
{
    class BrickMovementChecker : IBrickMovementChecker
    {
        public BrickMovementChecker()
        {

        }

        public bool Check(Brick brick, Vector2Int direction)
        {
            
            return true;
        }
    }
}
