namespace MoonflowCore.Runtime.Framework.Data
{
    /// <summary>
    /// 封装Flag系统
    /// </summary>
    public class MFFlag
    {
        private uint flag;

        public bool hasFlag(uint t)
        {
            return (flag & t) != 0;
        }

        public void AddFlag(uint t)
        {
            flag |= t;
        }

        public void RemoveFlag(uint t)
        {
            flag ^= t;
        }
    }
}