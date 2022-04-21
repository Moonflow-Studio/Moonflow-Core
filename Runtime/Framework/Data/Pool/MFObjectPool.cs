using System;
using System.Collections.Generic;
using Moonflow.Core;

namespace MoonflowCore.Runtime.Framework
{
    public class MFObjectPool<T, TT>
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
        public bool Remove(T key)
        {
            if (_pool.TryGetValue(key, out TT value))
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
        public bool GetAndRemove(T key, out TT v)
        {
            if (_pool.TryGetValue(key, out TT value))
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