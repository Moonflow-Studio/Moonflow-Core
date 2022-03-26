using System.Collections.Generic;
using Moonflow.Core;

namespace MoonflowCore.Runtime.Framework
{
    public abstract class MFSimplePool<T>: MFSingleton where T:new()
    {
        public string name;
        private Dictionary<int, T> _pool;
        
        public void Add(T value, int id)
        {
            _pool.Add(id, value);
        }
        public T GetValue(int hash)
        {
            if (_pool.TryGetValue(hash, out T value))
            {
                return value;
            }
            MFDebug.LogError($"No value in pool {name}");
            return default;
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