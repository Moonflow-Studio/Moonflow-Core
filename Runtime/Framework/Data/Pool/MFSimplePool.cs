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