using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickData
    {
        public Vector2Int LocalCenter { get;  set; }
        public Vector2Int GlobalCenter { get;  set; }
        public List<BrickPart> Shape { get;  set; }
        public ObjectPool<Transform> BlockPool { get;  set; }

        public BrickData(Transform blockContainer)
        {
            var blockPrefab = Resources.Load<GameObject>("BlockObject");
            BlockPool = BlockPool = new ObjectPool<Transform>(blockPrefab.transform, 16, blockContainer);
        }
    }
}
