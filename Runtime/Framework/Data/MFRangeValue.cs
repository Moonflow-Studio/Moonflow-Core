using System;

namespace Moonflow
{
    /// <summary>
    /// 封装范围性数值，拥有当前值和最大值
    /// </summary>
    /// <typeparam name="T">被封装的数值</typeparam>
    [Serializable]
    public struct MFRangeValue<T>
    {
        public T current;
        public T max;
    }
}