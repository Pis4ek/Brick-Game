using System.Collections;
using UnityEngine;
using UniRx;

namespace PlayMode.Map
{
    public class BlockView
    {
        private CoordinateConverter _converter;
        private BlockObject _blockObject;
        private Block _block;

        public BlockView(CoordinateConverter converter, BlockObject blockObject, Block block)
        {
            _converter = converter;
            _blockObject = blockObject;
            _block = block;

            SetStartValues();
            SubscribeToBlockEvents();
        }

        private void SetStartValues()
        {
            _blockObject.transform.position = _converter.MapCoordinatesToWorld(_block.Coordinates.Value);
            _blockObject.SetColor(_block.Color);
        }

        private void SubscribeToBlockEvents()
        {
            _block.Coordinates.Subscribe(value => 
            {
                var targetPosition = _converter.MapCoordinatesToWorld(_block.Coordinates.Value);
                _blockObject.MoveBlock(targetPosition, 0.15f); 
            });

            _block.OnDestroyedEvent += () => { _blockObject.Disactivate(); };
        }
    }
}