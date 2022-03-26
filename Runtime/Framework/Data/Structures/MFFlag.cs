namespace MoonflowCore.Runtime.Framework.Data
{
    /// <summary>
    /// 封装Flag系统
    /// </summary>
    public class MFFlag
    {
        private uint flag;

        /// <summary>
        /// 是否含有Flag
        /// </summary>
        /// <param name="t">指定Flag</param>
        /// <returns>返回检测结果</returns>
        public bool hasFlag(uint t)
        {
            return (flag & t) != 0;
        }

        /// <summary>
        /// 增加Flag
        /// </summary>
        /// <param name="t">flag</param>
        public void AddFlag(uint t)
        {
            flag |= t;
        }

        /// <summary>
        /// 移除flag
        /// </summary>
        /// <param name="t">flag</param>
        public void RemoveFlag(uint t)
        {
            flag ^= t;
        }

        public void Clear()
        {
            flag = 0;
        }
    }
}