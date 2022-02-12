using System.Collections.Generic;
using UnityEngine;

namespace MoonflowCore.Runtime.Framework.Data
{
    public class MFAssetBundleLoader
    {
        static Dictionary<string, AssetBundle> DicAssetBundle = new Dictionary<string, AssetBundle>(); 

        public static T LoadResource<T>(string assetBundleName, string assetBundleGroupName) where T : Object
        {
            if (string.IsNullOrEmpty(assetBundleGroupName))
            {
                return default(T);
            }

            if (!DicAssetBundle.TryGetValue(assetBundleGroupName, out AssetBundle assetbundle))
            {
                assetbundle = AssetBundle.LoadFromFile(GetStreamingAssetsPath() + assetBundleGroupName);//+ ".assetbundle"
                DicAssetBundle.Add(assetBundleGroupName, assetbundle);
            }
            object obj = assetbundle.LoadAssetAsync(assetBundleName, typeof(T));
            var one = obj as T; 
            return one;
        }

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

        public static string GetStreamingAssetsPath()
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