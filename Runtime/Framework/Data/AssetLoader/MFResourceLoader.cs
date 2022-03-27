using System.Collections.Generic;
using Moonflow.Core;
using UnityEngine;

namespace MoonflowCore.Runtime.Framework.Data
{
    public class MFResourceLoader
    {
        private static Dictionary<string, Object> DicAssets;
        /// <summary>
        /// 加载指定类型Resources资产
        /// </summary>
        /// <param name="resName">被加载的资源名</param>
        /// <param name="resPath">被加载的AssetBundle路径</param>
        /// <typeparam name="T">指定加载的资源类型</typeparam>
        /// <returns>返回指定资源（如为空则返回default）</returns>
        public static T LoadResource<T>(string resName, string resPath) where T : Object
        {
            if (!DicAssets.TryGetValue(resPath, out Object asset))
            {
                asset = Resources.Load<T>(resPath);
                DicAssets.Add(resPath, asset);
            }
            return (T)asset;
        }

        ~MFResourceLoader()
        {
            foreach (var pair in DicAssets)
            {
                Object.Destroy(pair.Value);
            }
            DicAssets.Clear();
        }
    }
}