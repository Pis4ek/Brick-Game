using PlayMode.Bricks;
using System;
using UnityEngine;

namespace PlayMode.Map
{
    public class DefaultBlock
    {
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
        public BlockType Type { get; set; }
        public int HP { get; set; } = 1;

        private GameObject _gameObject;

        public DefaultBlock(IReadonlyBrickPart block, GameObject gameObject)
        {
            GameObject = gameObject;
            Renderer.material = block.Renderer.material;
            GameObject.transform.position = block.GameObject.transform.position;
            Coordinates = block.Coordinates;
            Type = block.Type;
        }
    }
}
