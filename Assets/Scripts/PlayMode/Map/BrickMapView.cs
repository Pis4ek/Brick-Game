using System;
using System.Collections;
using UnityEngine;

namespace PlayMode.Map
{
    public class BrickMapView : MonoBehaviour
    {
        public bool IsInitialized { get; private set; } = false;

        private BlockMap _brickMap;

        public void Init(BlockMap brickMap)
        {
            _brickMap = brickMap;

            _brickMap.OnValueChanged += UpdateMapView;

            GenerateMap();

            IsInitialized = true;
        }

        private void GenerateMap()
        {
            
        }

        private void UpdateMapView()
        {
            
        }
    }
}