using System;
using System.Collections.Generic;
using Moonflow.Core;

namespace MoonflowCore.Runtime.Framework
{
    public abstract class MFObjectPool<T, TT>
    {
        public string name;
        private Dictionary<T, TT> _pool;
        public void Init(string name)
        {
            this.name = name;
            _pool = new Dictionary<T, TT>();
        }

        public void Add(T key, TT value)
        {
            _pool.Add(key, value);
        }
        public bool GetValue(T key, out TT t)
        {
            if (_pool.TryGetValue(key, out TT value))
            {
                t = value;
                return true;
            }
            MFDebug.LogError($"No value in pool {name}");
            t = default;
            return false;
        }

        public Dictionary<T, TT> Pool
        {
            get => _pool;
            // set => _pool = value;
        }
    }
}