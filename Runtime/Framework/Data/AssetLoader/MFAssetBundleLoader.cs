using System.Collections.Generic;
using UnityEngine;

namespace MoonflowCore.Runtime.Framework.Data
{
    /// <summary>
    /// 封装AssetBundle加载
    /// </summary>
    public class MFAssetBundleLoader
    {
        static Dictionary<string, AssetBundle> DicAssetBundle = new Dictionary<string, AssetBundle>(); 

        /// <summary>
        /// 加载指定类型AssetBundle资产
        /// </summary>
        /// <param name="resName">被加载的资源名</param>
        /// <param name="bundlePath">被加载的AssetBundle路径</param>
        /// <typeparam name="T">指定加载的资源类型</typeparam>
        /// <returns>返回指定资源（如为空则返回default）</returns>
        public static T LoadResource<T>(string resName, string bundlePath) where T : Object
        {
            if (string.IsNullOrEmpty(bundlePath))
            {
                return default(T);
            }

            if (!DicAssetBundle.TryGetValue(bundlePath, out AssetBundle assetbundle))
            {
                assetbundle = AssetBundle.LoadFromFile(GetStreamingAssetsPath() + bundlePath);//+ ".assetbundle"
                DicAssetBundle.Add(bundlePath, assetbundle);
            }
            object obj = assetbundle.LoadAssetAsync(resName, typeof(T));
            var one = obj as T; 
            return one;
        }

        /// <summary>
        /// 卸载AssetBundle包
        /// </summary>
        /// <param name="assetBundleGroupName">被卸载的AssetBundle名</param>
        public static void UnLoadResource(string assetBundleGroupName)
        {
            if (DicAssetBundle.TryGetValue(assetBundleGroupName, out AssetBundle assetbundle))
            {
                assetbundle.Unload(false);
                if (assetbundle != null)
                {
                    assetbundle = null;
                }
                DicAssetBundle.Remove(assetBundleGroupName);
                Resources.UnloadUnusedAssets();
            }
        }

        /// <summary>
        /// 获取指定平台StreamingAssets路径
        /// </summary>
        /// <returns>返回指定平台StreamingAssets路径</returns>
        private static string GetStreamingAssetsPath()
        {
            string StreamingAssetsPath =
#if UNITY_EDITOR
                Application.streamingAssetsPath + "/";
#elif UNITY_ANDROID
        "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
        Application.dataPath + "/Raw/";
#else
        string.Empty;
#endif
            return StreamingAssetsPath;
        }
    }
}