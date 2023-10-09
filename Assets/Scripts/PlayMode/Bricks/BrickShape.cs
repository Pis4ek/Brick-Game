using System.Collections.Generic;
using System;
using UnityEngine;
using PlayMode.Map;

namespace PlayMode.Bricks
{
    public class BrickShape : IReadOnlyBrickShape
    {
        public IReadOnlyList<IReadonlyBrickPart> BlocksShape => Blocks;
        public List<BrickPart> Blocks { get; set; } = new List<BrickPart>();
        public Vector2Int GlobalCenter { get; set; }
        public Vector2Int LocalCenter { get; set; }
        public Color Color { get; set; }

    }
}
