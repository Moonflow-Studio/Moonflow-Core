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
        public static MFAsyncLoader singleton;

        private void Awake()
        {
            singleton = this;
        }

        public static IEnumerator LoadFromResourceAsync<T>(string bundlePath, string name, MFAssetData assetInfo)
            where T : UnityEngine.Object
        {
            var resLoadState = Resources.LoadAsync<T>(bundlePath + name);
            yield return resLoadState.isDone;
            assetInfo.asset = resLoadState.asset;
        }
    }
}