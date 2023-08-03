using PlayMode.Map;
using System.Collections;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickPart : IReadonlyBrickPart
    {
        public Vector2Int LocalCoordinates { get; set; }
        public Vector2Int Coordinates { get; set; }
        public BlockType Type { get; private set; }
        public GameObject GameObject
        {
            get { return _gameObject; }
            set
            {
                _gameObject = value;
                Renderer = _gameObject.GetComponent<MeshRenderer>();
            }
        }
        public MeshRenderer Renderer { get; private set; }

        private GameObject _gameObject;

        public BrickPart()
        {
            float value = Random.Range(0f, 100f);

            if (value < 0.1f)
                Type = BlockType.Obsidian;
            else if (value < 10.4f)
                Type = BlockType.Lava;
            else if (value < 11.4f)
                Type = BlockType.Ice;
            else if (value < 12.4f)
                Type = BlockType.Stone;
            else
                Type = BlockType.Default;
        }

        public BrickPart(BrickPart brickPart)
        {
            LocalCoordinates = brickPart.LocalCoordinates;
            Coordinates = brickPart.Coordinates;
            GameObject = brickPart.GameObject;
            Type = brickPart.Type;
        }
    }
}