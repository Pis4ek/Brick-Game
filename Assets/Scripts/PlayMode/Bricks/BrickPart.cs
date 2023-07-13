using System.Collections;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickPart
    {
        public Vector2Int Coordinates;
        public bool IsUsed;

        public BrickPart(Vector2Int coordinates)
        {
            Coordinates = coordinates;
        }
    }
}