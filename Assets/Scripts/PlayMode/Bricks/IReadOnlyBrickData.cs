using System.Collections.Generic;
using System;
using UnityEngine;

namespace PlayMode.Bricks
{
    public interface IReadOnlyBrickData
    {
        public event Action OnBrickLandedEvent;

        public Vector2Int LocalCenter { get; }
        public Vector2Int GlobalCenter { get;}
        public List<BrickPart> Shape { get; }
    }
}
