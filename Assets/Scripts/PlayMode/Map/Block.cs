using System;
using UnityEngine;

namespace PlayMode.Map
{
    public class Block
    {
        public event Action OnValueChangedEvent;
        public event Action OnDestroyedEvent;

        public Vector2Int Coordinates { 
            get { return _coordinates; }
            set 
            {
                _coordinates = value;
                OnValueChangedEvent?.Invoke();
            }
        }

        private Vector2Int _coordinates;

        public Block(Vector2Int coordinates)
        {
            Coordinates = coordinates;
        }

        public void Destroy()
        {
            OnDestroyedEvent?.Invoke();
        }
    }
}
