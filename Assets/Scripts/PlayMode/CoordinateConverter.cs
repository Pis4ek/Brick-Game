using UnityEngine;

namespace PlayMode
{
    public class CoordinateConverter
    {
        private Vector3 _worldStartMap;
        private float _cellSize;

        public CoordinateConverter(float cellSize, Vector3 worldStartMap)
        {
            _cellSize = cellSize;
            _worldStartMap = worldStartMap;
        }

        public Vector3 MapCoordinatesToWorld(Vector2Int mapCoordinates)
        {
            var offsetX = mapCoordinates.x * _cellSize;
            var offsetY = mapCoordinates.y * _cellSize;
            return new Vector3(_worldStartMap.x + offsetX, _worldStartMap.y - offsetY, _worldStartMap.z);
        }

        public float MapHeightToWorld(int height)
        {
            return _worldStartMap.y - height * _cellSize;
        }

        public float MapWidthToWorld(int width)
        {
            return _worldStartMap.x + width * _cellSize;
        }
    }
}
