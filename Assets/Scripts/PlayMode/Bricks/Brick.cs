using System.Collections;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class Brick : MonoBehaviour
    {
        public BrickPart [,] BrickShape { get; private set; }

        public Brick Init()
        {
            return this;
        }

        public void Move(Vector2Int direction)
        {
            transform.position += new Vector3(direction.x, direction.y, 0);
        }
    }
}