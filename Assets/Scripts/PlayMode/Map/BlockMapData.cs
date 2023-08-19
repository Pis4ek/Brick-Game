using UnityEngine;

namespace PlayMode.Map
{
    public class BlockMapData
    {
        public BlockLine[] GameMap { get; set; }
        public Vector2Int MapSize { get; private set; } = new Vector2Int(10, 15);
        public Vector3 WorldStartMap { get; private set; } = new Vector3(-4.5f, 6.5f, -0.5f);
        public float CellSize { get; private set; } = 1f;

        public ObjectPool<BlockView> _objectPool;
        public int _destroyedLinesCount = 0;

        public BlockMapData(Transform blockContainer)
        {
            GameMap = new BlockLine[MapSize.y];

            var prefab = Resources.Load<GameObject>("BlockObject").GetComponent<BlockView>();
            _objectPool = new ObjectPool<BlockView>(prefab, 50, blockContainer);
            _objectPool.AutoExpand = true;
        }
    }
}