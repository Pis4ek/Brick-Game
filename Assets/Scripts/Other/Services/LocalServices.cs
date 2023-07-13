using System;
using System.Collections.Generic;

namespace Services
{
    public class LocalServices : IServiceLocator
    {
        private Dictionary<Type, IService> _servicesMap;

        public LocalServices()
        {
            _servicesMap = new Dictionary<Type, IService>();
        }

        public void Add<T>(T service) where T : IService
        {
            var type = typeof(T);
            if (_servicesMap.ContainsKey(type))
            {
                throw new Exception($"LocalServices has already {type} service and can not add new one.");
            }
            _servicesMap.Add(type, service);
        }

        public void Remove<T>() where T : IService
        {
            var type = typeof(T);
            if (!_servicesMap.ContainsKey(type))
            {
                throw new Exception($"LocalServices has not {type} service and can not remove it.");
            }
            _servicesMap.Remove(type);
        }

        public T GetService<T>() where T : IService
        {
            var type = typeof(T);
            if (HasSuchService<T>())
            {
                return (T)_servicesMap[type];
            }
            throw new Exception($"LocalServices has not {type} service and can not get it.");
        }

        public bool TryGetService<T>(out T service) where T : IService
        {
            var type = typeof(T);
            if (HasSuchService<T>())
            {
                service = (T)_servicesMap[type];
                return true;
            }
            service = default;
            return false;
        }

        public bool HasSuchService<T>() where T : IService
        {
            return _servicesMap.ContainsKey(typeof(T));
        }
    }
}