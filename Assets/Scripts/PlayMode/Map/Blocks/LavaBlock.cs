using PlayMode.Bricks;
using System;
using UnityEngine;

namespace PlayMode.Map
{
    public class LavaBlock : DefaultBlock
    {
        private int _counter;

        public LavaBlock(IReadonlyBrickPart block, GameObject gameObject, IBlockMapActions actions) : base(block, gameObject)
        {
            _counter = 5;

            actions.OnBrickLandedEvent += CheckTransformation;
        }

        private void CheckTransformation()
        {
            _counter--;

            if(_counter == 0)
            {
                Renderer.material.color = Color.black;
                HP = 3;
            }
        }
    }
}
