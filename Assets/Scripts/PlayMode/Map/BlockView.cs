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
                    var effect = _blockMapData._vfxPool.GetElement();
                    effect.transform.position = _converter.MapCoordinatesToWorld(_block.Coordinates) - new Vector3(0, 0, 1);
                    effect.Play();
                    _block = null;
                    GameObject.SetActive(false);
                };
            }
        }
        public GameObject GameObject { get; private set; }
        public MeshRenderer Renderer { get; private set; }

        private Block _block;
        private CoordinateConverter _converter;
        BlockMapData _blockMapData;

        private void Awake()
        {
            GameObject = gameObject;
            Renderer = GameObject.GetComponent<MeshRenderer>();
        }

        public void Init(Block block, IReadonlyBrickPart blockData, CoordinateConverter converter, BlockMapData blockMapData)
        {
            Renderer.material.color = blockData.Renderer.material.color;
            GameObject.transform.position = converter.MapCoordinatesToWorld(blockData.Coordinates);
            Block = block;
            _converter = converter;
            _blockMapData = blockMapData;
        }

        private void MoveBlock()
        {
            transform.DOMove(_converter.MapCoordinatesToWorld(_block.Coordinates), 0.15f);
        }
    }
}