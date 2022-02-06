#define MF_DEBUG
using UnityEngine;
namespace Moonflow.Core
{
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

        public static void ABLoadError(string objName, string abPath)
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