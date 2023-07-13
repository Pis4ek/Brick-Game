using Services.ConfigServiceComponents;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ConfigService : IService
    {
        private Dictionary<Type, ConfigContainer<IConfig>> _configContainersMap;
        private Dictionary<Type, ConfigContainer<IConfig>> _permanentConfigMap;


        public ConfigService()
        {
            _configContainersMap = new Dictionary<Type, ConfigContainer<IConfig>>();
            _permanentConfigMap = new Dictionary<Type, ConfigContainer<IConfig>>();
        }

        public void LoadConfigsForScene(string sceneName)
        {
            _configContainersMap.Clear();

            var path = "Configs\\" + sceneName;
            var configs = Resources.LoadAll(path, typeof(IConfig));

            if (configs.Length > 0)
            {
                foreach (var c in configs)
                {
                    var config = (IConfig)c;
                    AddConfig(config, config.KeyID);
                }
            }
            else
            {
                Debug.LogWarning($"ConfigService has not any configs for scene \"{sceneName}\"");
            }
        }

        public void AddConfig(IConfig config, string key) 
        {
            var type = config.ConfigType;
            if (HasSuchContainer(type))
            {
                _configContainersMap[type].Add(config, key);
                return;
            }
            var container = new ConfigContainer<IConfig>();
            container.Add(config, key);
            _configContainersMap.Add(type, container);
        }
        public void AddPermanentConfig(IConfig config, string key)
        {
            var type = config.ConfigType;
            if (HasSuchPermanentContainer(type))
            {
                _permanentConfigMap[type].Add(config, key);
                return;
            }
            var container = new ConfigContainer<IConfig>();
            container.Add(config, key);
            _permanentConfigMap.Add(type, container);
        }

        public void RemoveConfig(Type type, string key) 
        {
            if (HasSuchContainer(type))
            {
                _configContainersMap[type].Remove(key);
            }
            throw new Exception($"ConfigService has not ConfigContainer<{type}> and can not remove config of such type.");
        }
        public void RemovePermanentConfig(Type type, string key)
        {
            if (HasSuchPermanentContainer(type))
            {
                _permanentConfigMap[type].Remove(key);
            }
            throw new Exception($"ConfigService has not ConfigContainer<{type}> and can not remove config of such type.");
        }

        public bool TryGetConfig<T>(string key, out T config) where T : class, IConfig
        {
            var type = typeof(T);
            if (HasSuchConfig(type, key))
            {
                config = (T)_configContainersMap[type].Get(key);
                return true;
            }
            config = null;
            return false;
        }
        public bool TryGetPermanentConfig<T>(string key, out T config) where T : class, IConfig
        {
            var type = typeof(T);
            if (HasSuchPermanentConfig(type, key))
            {
                config = (T)_permanentConfigMap[type].Get(key);
                return true;
            }
            config = null;
            return false;
        }

        public bool TryGetContainer(Type type, out ConfigContainer<IConfig> config)
        {
            if (HasSuchContainer(type))
            {
                config = _configContainersMap[type];
                return true;
            }
            config = null;
            return false;
        }
        public bool TryGetPermanentContainer(Type type, out ConfigContainer<IConfig> config)
        {
            if (HasSuchPermanentContainer(type))
            {
                config = _permanentConfigMap[type];
                return true;
            }
            config = null;
            return false;
        }

        public bool HasSuchConfig(Type type, string key)
        {
            if (HasSuchContainer(type))
            {
                if (_configContainersMap[type].HasSuchKey(key))
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasSuchPermanentConfig(Type type, string key)
        {
            if (HasSuchPermanentContainer(type))
            {
                if (_permanentConfigMap[type].HasSuchKey(key))
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasSuchContainer(Type type)
        {
            return _configContainersMap.ContainsKey(type);
        }
        private bool HasSuchPermanentContainer(Type type)
        {
            return _permanentConfigMap.ContainsKey(type);
        }
    }
}
