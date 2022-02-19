using System;

namespace Moonflow
{
    [Serializable]
    public struct MFRangeValue<T>
    {
        public T current;
        public T max;
    }
}