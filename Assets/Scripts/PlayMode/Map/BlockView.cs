using DG.Tweening;
using PlayMode.Bricks;
using System.Collections;
using UnityEngine;

namespace PlayMode.Map
{
    public class BlockView : MonoBehaviour
    {
        public Block Block
        {
            get { return _block; }
            set
            {
                _block = value;
                _block.OnValueChangedEvent += MoveBlock;
                _block.OnDestroyedEvent += delegate ()
                {
                    _block = null;
                    GameObject.SetActive(false);
                };
            }
        }
        public GameObject GameObject { get; private set; }
        public MeshRenderer Renderer { get; private set; }

        private Block _block;
        private CoordinateConverter _converter;

        private void Awake()
        {
            GameObject = gameObject;
            Renderer = GameObject.GetComponent<MeshRenderer>();
        }

        public void Init(Block block, IReadonlyBrickPart blockData, CoordinateConverter converter)
        {
            Renderer.material.color = blockData.Renderer.material.color;
            GameObject.transform.position = converter.MapCoordinatesToWorld(blockData.Coordinates);
            Block = block;
            _converter = converter;
        }

        private void MoveBlock()
        {
            transform.DOMove(_converter.MapCoordinatesToWorld(_block.Coordinates), 0.15f);
        }
    }
}