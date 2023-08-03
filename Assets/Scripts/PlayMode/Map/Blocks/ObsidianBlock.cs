using PlayMode.Bricks;
using UnityEngine;

namespace PlayMode.Map
{
    public class ObsidianBlock : DefaultBlock
    {
        public ObsidianBlock(IReadonlyBrickPart block, GameObject gameObject) : base(block, gameObject)
        {
            HP = 3;
        }
    }
}
