#define MF_DEBUG
using UnityEngine;
namespace Moonflow.Core
{
    /// <summary>
    /// 自用Debug封装
    /// </summary>
    public static class MFDebug
    {
        public static void Log(string context)
        {
#if MF_DEBUG
            Debug.Log(context);
#endif
        }
        
        public static void Log(float context)
        {
#if MF_DEBUG
            Debug.Log(context.ToString());
#endif
        }

        public static void LogError(string context)
        {
#if MF_DEBUG
            Debug.LogError(context);    
#endif
        }
        
        /// <summary>
        /// Assetbundle专用LogError
        /// </summary>
        /// <param name="objName">被加载的对象</param>
        /// <param name="abPath">被加载的路径</param>
        public static void LoadErrorAB(string objName, string abPath)
        {
            LogError($"未能加载到资源{objName}, 从{abPath}");
        }
        public static void LogWarning(string context)
        {
#if MF_DEBUG
            Debug.LogWarning(context);    
#endif
        }
    }
}