using System;
using UnityEngine;

namespace PlayMode.Map
{
    public class Block
    {
        public Vector2Int Coordinates { get; private set; }
        public GameObject GameObject { get; private set; }

        public Block(Vector2Int coordinates, GameObject gameObject)
        {
            Coordinates = coordinates;
            GameObject = gameObject;
        }
    }
}
