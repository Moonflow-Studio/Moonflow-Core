using System;
using System.Collections.Generic;
using Moonflow.Core;

namespace MoonflowCore.Runtime.Framework
{
    public abstract class MFObjectPool<T, TT> : MFSingleton
    {
        public string name;
        private Dictionary<T, TT> _pool;
        
        public void Add(T key, TT value)
        {
            _pool.Add(key, value);
        }
        public TT GetValue(T key)
        {
            if (_pool.TryGetValue(key, out TT value))
            {
                return value;
            }
            MFDebug.LogError($"No value in pool {name}");
            return default;
        }

        public Dictionary<T, TT> Pool
        {
            get => _pool;
            // set => _pool = value;
        }
    }
}