using UniRx;
using UnityEngine;
using UnityEngine.VFX;

namespace PlayMode.Map
{
    public class BlockMapData
    {
        public Vector2Int MapSize { get; private set; } = new Vector2Int(10, 15);
        public Vector3 WorldStartMap { get; private set; } = new Vector3(-4.5f, 6.5f, -0.5f);
        public float CellSize { get; private set; } = 1f;

        public BlockMapData(Transform blockContainer, VisualEffect _destroingEffect)
        {
        }
    }
}