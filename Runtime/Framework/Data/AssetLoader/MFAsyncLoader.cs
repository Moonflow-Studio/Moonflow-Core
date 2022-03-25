using System;
using System.Collections;
using UnityEngine;

namespace MoonflowCore.Runtime.Framework.Data
{
    /// <summary>
    /// 资产异步加载器
    /// </summary>
    public class MFAsyncLoader: MonoBehaviour
    {
        private static MFAsyncLoader singleton;

        private void Awake()
        {
            singleton = this;
        }
        
        /// <summary>
        /// 异步加载bundle资源
        /// </summary>
        /// <param name="bundlePath">bundle路径</param>
        /// <param name="name">资源名</param>
        /// <param name="assetInfo">资源控制信息</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerator LoadFromResourceAsync<T>(string bundlePath, string name, MFAssetData assetInfo)
            where T : UnityEngine.Object
        {
            var resLoadState = Resources.LoadAsync<T>(bundlePath + name);
            yield return resLoadState.isDone;
            assetInfo.asset = resLoadState.asset;
        }
    }
}