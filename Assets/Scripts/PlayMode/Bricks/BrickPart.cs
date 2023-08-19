using PlayMode.Map;
using System.Collections;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class BrickPart : IReadonlyBrickPart
    {
        public Vector2Int LocalCoordinates { get; set; }
        public Vector2Int Coordinates { get; set; }
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
        }

        public BrickPart(BrickPart brickPart)
        {
            LocalCoordinates = brickPart.LocalCoordinates;
            Coordinates = brickPart.Coordinates;
            GameObject = brickPart.GameObject;
        }
    }
}