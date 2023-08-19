using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayMode
{
    public class BrickConfigLoader
    {
        private List<BrickConfig> _bricksConfigs;

        public BrickConfigLoader()
        {
            var json = Resources.Load<TextAsset>("BricksInfo 3");
            if (json == null)
            {
                throw new Exception($"{GetType()} has not found BrickConfig for default bricks.");
            }
            _bricksConfigs = JsonConvert.DeserializeObject<List<BrickConfig>>(json.ToString());
        }

        public BrickConfig GetConfig(int index)
        {
            if(index < 0 || index > _bricksConfigs.Count - 1)
            {
                throw new Exception($"OutOfRangeExeption. {GetType()} has not BrickConfig with index {index}.");
            }
            return _bricksConfigs[index];
        }

        public BrickConfig GetRandomConfig()
        {
            return _bricksConfigs[GetRandomIndex()];
        }

        public int GetRandomIndex()
        {
            return UnityEngine.Random.Range(0, _bricksConfigs.Count);
        }
    }
}