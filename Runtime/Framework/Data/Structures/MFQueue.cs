using System;
using System.Collections;
using System.Collections.Generic;

namespace MoonflowCore.Runtime.Framework.Data.Structures
{
    public class MFQueue<T> where T : new()
    {
        private Queue<T> _data;
        public MFQueue()
        {
            _data = new Queue<T>();
        }

        public T Get()
        {
            if (_data.Count > 0)
            {
                return _data.Dequeue();
            }
            return new T();
        }

        public void Push(T newData)
        {
            _data.Enqueue(newData);
        }
    }
}