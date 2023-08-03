using PlayMode.Bricks;
using UnityEngine;

namespace PlayMode.Map
{
    public class StoneBlock : DefaultBlock
    {
        public StoneBlock(IReadonlyBrickPart block, GameObject gameObject) : base(block, gameObject)
        {
            HP = 2;
        }
    }
}