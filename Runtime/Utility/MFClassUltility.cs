using System;
using System.Reflection;

namespace Moonflow.Utility
{
    /// <summary>
    /// 类辅助功能
    /// </summary>
    public class MFClassUltility
    {
        /// <summary>
        /// 深拷贝一个自定类，返回拷贝完成的对象
        /// </summary>
        /// <param name="obj">被拷贝的对象</param>
        /// <typeparam name="T">被拷贝对象的类型</typeparam>
        /// <returns>返回被拷贝的对象</returns>
        public static T DeepCopyByReflect<T>(T obj)
        {
            if (obj is string || obj.GetType().IsValueType) return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }
        /// <summary>
        /// 获取枚举的数量
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        /// <returns>返回数量</returns>
        public static int GetEnumNum(Type enumType)
        {
            return enumType.GetFields(BindingFlags.Public | BindingFlags.Static).Length;
        }
    }
}