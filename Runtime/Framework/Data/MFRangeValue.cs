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
        /// <summary>
        /// 当前值
        /// </summary>
        public T current;
        /// <summary>
        /// 最大值
        /// </summary>
        public T max;
    }
}