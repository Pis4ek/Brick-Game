using System.Collections.Generic;
using System;
using UnityEngine;

namespace PlayMode.Bricks
{
    public interface IReadOnlyBrickShape
    {
        public IReadOnlyList<IReadonlyBrickPart> BlocksShape { get; }
        public Vector2Int GlobalCenter { get; }
        public Vector2Int LocalCenter { get; }
        public Color Color { get; }
    }
}
