using System.Collections.Generic;
using System;
using UnityEngine;
using PlayMode.Map;

namespace PlayMode.Bricks
{
    public class BrickData : IReadOnlyBrickData
    {
        public event Action OnBrickLandedEvent;

        public Vector2Int LocalCenter { get;  set; }
        public Vector2Int GlobalCenter { get;  set; }
        public Color Color { get; set; }
        public List<BrickPart> Shape { get;  set; }
        public List<Vector2Int> FullDownPosition { get; set; } = new List<Vector2Int>(16);
        public bool IsLanded { get; set; }

        public void SendBrickLandedEvent()
        {
            IsLanded = false;
            OnBrickLandedEvent?.Invoke();
        }
    }
}
