using System.Collections.Generic;
using UnityEngine;

namespace PlayMode.Bricks
{
    public interface IReadonlyBrickShape
    {
        public IReadOnlyDictionary<Vector2Int, IReadonlyBrickPart> ReadonlyShape { get; }
    }
}
