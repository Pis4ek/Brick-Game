using System;
using UnityEngine;
using UniRx;

namespace PlayMode.Map
{
    public class Block
    {
        public event Action OnDestroyedEvent;

        public ReactiveProperty<Vector2Int> Coordinates { get; private set; }
        public Color Color { get; private set; }

        public Block(Vector2Int coordinates, Color color)
        {
            Coordinates = new ReactiveProperty<Vector2Int>();
            Coordinates.Value = coordinates;
            Color = color;
        }

        public void Destroy()
        {
            OnDestroyedEvent?.Invoke();
            Coordinates.Dispose();
        }

        public override string ToString()
        {
            return $"Block at ({Coordinates.Value.x}, {Coordinates.Value.y})";
        }
    }
}
