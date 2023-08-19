using UnityEngine;

namespace PlayMode.Bricks
{
    public interface IReadonlyBrickPart
    {
        public Vector2Int LocalCoordinates { get;}
        public Vector2Int Coordinates { get; }
        public GameObject GameObject { get; }
        public MeshRenderer Renderer { get; }
    }
}
