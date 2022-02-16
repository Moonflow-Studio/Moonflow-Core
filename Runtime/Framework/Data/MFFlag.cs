namespace MoonflowCore.Runtime.Framework.Data
{
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