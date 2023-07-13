using System;
using System.Collections.Generic;

namespace Services.ConfigServiceComponents
{
    public class ConfigContainer<T> where T : class, IConfig
    {
        public int Count => _configMap.Count;

        private Dictionary<string, T> _configMap;

        public ConfigContainer()
        {
            _configMap = new Dictionary<string, T>();
        }

        public void Add(T config, string key) 
        {
            if (_configMap.ContainsKey(key))
            {
                throw new Exception($"ConfigContainer has already config with key \"{key}\".");
            }
            _configMap.Add(key, config);
        }

        public void Remove(string key) 
        {
            if (_configMap.ContainsKey(key))
            {
                _configMap.Remove(key);
            }
            throw new Exception($"ConfigContainer has not config with key \"{key}\".");
        }

        public T Get(string key)
        {
            if (_configMap.ContainsKey(key))
            {
                return _configMap[key];
            }
            return null;
        }

        public T[] GetAll()
        {
            List<T> configs = new List<T>(Count);
            foreach(var c in _configMap)
            {
                configs.Add(c.Value);
            }
            return configs.ToArray();
        }

        public bool HasSuchKey(string key)
        {
            return _configMap.ContainsKey(key);
        }
    }
}
