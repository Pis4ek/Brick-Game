using PlayMode.Bricks;
using System;
using UnityEngine;

namespace PlayMode.Map
{
    public class BlockMap
    {
        public event Action OnValueChanged;

        public bool[,] GameMap { get; private set; }
        public Vector2Int MapSize { get; private set; }
        public IBrickMovementChecker MovementChecker { get; private set; }


        public BlockMap()
        {
            MapSize = new Vector2Int(10, 15);
            GameMap = new bool[MapSize.x, MapSize.y];
            MovementChecker = new BrickMovementChecker();
        }

        public bool CheckMove(Brick brick, Vector2Int direction)
        {
            return MovementChecker.Check(brick, direction);
        }

/*        [ContextMenu("Add rand")]
        private void AddRand()
        {
            var randPos = new Vector2Int(UnityEngine.Random.Range(0, MapSize.x),
                UnityEngine.Random.Range(0, MapSize.y));
            GameMap[randPos.x, randPos.y] = new Block();

            OnValueChanged?.Invoke();
        }*/
    }
}
