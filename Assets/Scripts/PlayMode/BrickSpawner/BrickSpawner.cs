using PlayMode.Bricks;
using System;
using UnityEngine;

namespace PlayMode
{
    public class BrickSpawner : MonoBehaviour
    {
        [SerializeField] GameObject _brickPrefab;

        public void Spawn()
        {
            var go = Instantiate(_brickPrefab, transform.position, Quaternion.identity, transform);
            InitBrick(go.GetComponent<Brick>());
        }

        private void InitBrick(Brick brick)
        {
            for(int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    brick.BrickShape[x, y] = new BrickPart(new Vector2Int(x + 3, y));
                }
            }
        }
    }
}
