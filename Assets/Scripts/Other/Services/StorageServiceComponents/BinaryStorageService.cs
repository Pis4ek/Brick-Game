using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Services.Storage
{
    public class BinaryStorageService : IStorageService
    {
        private BinaryFormatter _binaryFormatter = new BinaryFormatter();

        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = BuildPath(key);

            FileStream fileStream = File.Create(path);
            _binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();

            callback?.Invoke(true);
        }

        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);

            FileStream fileStream = File.Open(path, FileMode.Open);
            T data = (T)_binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            callback?.Invoke(data);
        }

        private string BuildPath(string key)
        {
            key += ".json";
            return Path.Combine(Application.persistentDataPath, key);
        }
    }
}

