using System.Collections;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickInput : MonoBehaviour
    {
        private Brick _brick;

        private void Start()
        {
            _brick = GetComponent<Brick>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _brick.Move(Vector2Int.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _brick.Move(Vector2Int.down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _brick.Move(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _brick.Move(Vector2Int.right);
            }
        }
    }
}