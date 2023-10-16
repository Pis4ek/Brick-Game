using PlayMode.Map;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickFallingView : MonoBehaviour
    {
        private ObjectPool<BlockObject> _blocksPool;
        private CoordinateConverter _converter;
        private Brick _brick;

        public BrickFallingView Init(CoordinateConverter converter, Brick brick)
        {
            _converter = converter;
            _brick = brick;

            CreatePool();

            _brick.OnFullDownPositionUpdatedEvent += UpdateBlocks;

            return this;
        }

        private void CreatePool()
        {
            var prefabGO = Resources.Load<GameObject>("TransparentBlockObject");
            var prefab = prefabGO.GetComponent<BlockObject>();
            _blocksPool = new ObjectPool<BlockObject>(prefab, 16, transform, "Transparent_block");
        }

        private void UpdateBlocks(List<Vector2Int> position)
        {
            _blocksPool.HideAllElements();
            var color = _brick.Shape.Color;

            for (int i = 0; i < _brick.Shape.BlocksShape.Count; i++)
            {
                var obj = _blocksPool.GetElement();
                obj.transform.position = _converter.MapCoordinatesToWorld(position[i]);
                obj.Renderer.sharedMaterial.color = new Color(color.r, color.g, color.b, 0.2f);
            }
        }
    }
}