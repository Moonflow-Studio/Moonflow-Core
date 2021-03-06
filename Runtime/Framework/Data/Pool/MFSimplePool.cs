using System.Collections.Generic;
using Moonflow.Core;

namespace MoonflowCore.Runtime.Framework
{
    public class MFSimplePool<T>
    {
        public string name;
        private Dictionary<int, T> _pool;
        public MFSimplePool()
        {
            _pool = new Dictionary<int, T>();
        }
        public void Add(T value, int id)
        {
            _pool.Add(id, value);
        }
        public bool Remove(int key)
        {
            if (_pool.TryGetValue(key, out T value))
            {
                _pool.Remove(key);
                
                return true;
            }
            else
            {
                MFDebug.LogError($"pool {name} don't have item {key.ToString()}");
                return false;
            }
        }
        public bool GetAndRemove(int key, out T v)
        {
            if (_pool.TryGetValue(key, out T value))
            {
                _pool.Remove(key);
                v = value;
                return true;
            }
            else
            {
                MFDebug.LogError($"pool {name} don't have item {key.ToString()}");
                v = default;
                return false;
            }
        }
        
        public bool GetValue(int hash, out T target)
        {
            if (_pool.TryGetValue(hash, out T value))
            {
                target = value;
                return true;
            }
            MFDebug.LogError($"No value in pool {name}");
            target = default;
            return false;
        }

        public bool Contains(T value)
        {
            return _pool.ContainsValue(value);
        }
        public Dictionary<int, T> Pool
        {
            get => _pool;
        }
    }
}