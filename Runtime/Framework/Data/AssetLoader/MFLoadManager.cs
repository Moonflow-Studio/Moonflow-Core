using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Moonflow.Core;
using MoonflowCore.Runtime.Framework.Data;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 封装加载管理器，单循环基于场景加载
/// </summary>
public class MFLoadManager : MFSingleton<MFLoadManager>, IMFSceneCycle
{
    private Dictionary<int, MFAssetData> _assetDict;
    private Dictionary<string, AssetBundle> _loadedBundle;
    
    /// <summary>
    /// 加载回调委托
    /// </summary>
    public delegate void LoadCallback(Object asset);

    /// <summary>
    /// 初始化
    /// </summary>
    public MFLoadManager()
    {
        Init();
    }

    private void Init()
    {
        _assetDict = new Dictionary<int, MFAssetData>();
        _loadedBundle = new Dictionary<string, AssetBundle>();
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="resInfo"></param>
    /// <param name="immediate">是否立刻加载（否则异步加载）</param>
    /// <param name="cb">加载完毕需要执行的回调</param>
    /// <typeparam name="T">被加载的类型</typeparam>
    /// <returns></returns>
    public static T Load<T>(MFResInfo resInfo, bool immediate = false, LoadCallback cb = null) where T:UnityEngine.Object
    {
        string combinedpath = resInfo.Path + resInfo.Name;
        int hash = GetHash(combinedpath);
        if (GetInstance()._assetDict.TryGetValue(hash, out MFAssetData data))
        {
            data.refCount++;
            GetInstance()._assetDict[hash] = data;
            return data.asset as T;
        }
        else
        {
            MFAssetData assetInfo = new MFAssetData()
            {
                resInfo = new MFResInfo(resInfo),
                hash = hash,
            };
            //TODO:异步加载asset
            // if (immediate)
            // {
#if UNITY_EDITOR
                RealLoadFromResource<T>(resInfo, ref assetInfo);
#else
                RealLoadFromBundle<T>(bundlePath, name, ref assetInfo);
#endif
//             }
//             else
//             {
// #if UNITY_EDITOR
//                 RealLoadFromResourcesAsync<T>(bundlePath, name, ref assetInfo);
// #else
//                 RealLoadFromBundleAsync(bundlePath, name, ref assetInfo);
// #endif
//             }
            cb?.Invoke(assetInfo.asset);
            assetInfo.refCount = 1;
            GetInstance()._assetDict.Add(hash, assetInfo);
            return assetInfo.asset as T;
        }
    }

    // public static T DirectLoad<T>(T obj) where T:Object
    // {
    //     int hash = GetHash(obj.name);
    //     if (GetInstance()._assetDict.TryGetValue(hash, out MFAssetData data))
    //     {
    //         data.refCount++;
    //         GetInstance()._assetDict[hash] = data;
    //         return data.asset as T;
    //     }
    //     else
    //     {
    //         MFAssetData assetInfo = new MFAssetData()
    //         {
    //             resInfo = new MFResInfo(obj.name, ""),
    //             hash = hash,
    //         };
    //     }
    // }

    private static void RealLoadFromBundle(string bundlePath, string name, ref MFAssetData assetInfo)
    {
        GetInstance()._loadedBundle.TryGetValue(GetStreamingAssetsPath() + bundlePath, out AssetBundle bundle);
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(GetStreamingAssetsPath() + bundlePath);
            GetInstance()._loadedBundle.Add(bundlePath, bundle);
        }
        assetInfo.asset = bundle.LoadAsset(name);
    }
    private static void RealLoadFromResource<T>(MFResInfo resInfo, ref MFAssetData assetInfo) where T:Object
    {
        assetInfo.asset = Resources.Load<T>(resInfo.Path + resInfo.Name);
    }

    private static void RealLoadFromBundleAsync(string bundlePath, string name, ref MFAssetData assetInfo)
    {
        GetInstance()._loadedBundle.TryGetValue(GetStreamingAssetsPath() + bundlePath, out AssetBundle bundle);
        if (bundle == null)
        {
            var bundleLoadState = AssetBundle.LoadFromFileAsync(GetStreamingAssetsPath() + bundlePath);
            if (bundleLoadState.isDone)
            {
                var loadAssetState = bundleLoadState.assetBundle.LoadAssetAsync(name);
                if (loadAssetState.isDone)
                {
                    assetInfo.asset = loadAssetState.asset;
                }
                GetInstance()._loadedBundle.Add(bundlePath, bundle);
            }
        }
        else
        {
            var loadAssetState = bundle.LoadAssetAsync(name);
            if (loadAssetState.isDone)
            {
                assetInfo.asset = loadAssetState.asset;
            }
        }
    }
    private static void RealLoadFromResourcesAsync<T>(string bundlePath, string name, ref MFAssetData assetInfo) where T:Object
    {
        var resLoadState = Resources.LoadAsync<T>(bundlePath + name);
        if (resLoadState.isDone)
        {
            assetInfo.asset = resLoadState.asset;
        }
    }
    /// <summary>
    /// 加载管理器进场景时的操作
    /// </summary>
    public void OnEnterScene()
    {
        // throw new NotImplementedException();
    }

    /// <summary>
    /// 加载管理器出场景时的操作
    /// </summary>
    public void OnLeaveScene()
    {
        UnloadBundles();
    }

    public static void UnloadAsset(MFResInfo resInfo)
    {
        string combinedpath = resInfo.Path + resInfo.Name;
        int hash = GetHash(combinedpath);
        if (GetInstance()._assetDict.TryGetValue(hash, out MFAssetData data))
        {
            data.refCount--;
            if (data.refCount == 0)
            {
                GetInstance()._assetDict.Remove(hash);
                data.Dispose();
            }
        }
        else
        {
            MFDebug.LogError($"找不到实例{resInfo.Name}");
        }
    }
    private static void UnloadBundles()
    {
        foreach (var bundlePair in GetInstance()._loadedBundle)
        {
            bundlePair.Value.Unload(false);
        }
        GetInstance()._loadedBundle.Clear();
    }


    private static int GetHash(string str)
    {
        
        // if (GetInstance().hashDict.TryGetValue(str, out int value))
        // {
        //     return value;
        // }

        //C#原生hash算法
        int hash = str.GetHashCode();
        
        // if (str == null) return 0;
        // int hash = 0;
        // for (int i = 0; i < str.Length; i++)
        // {
        //     char c = str[i];
        //     if ('A' <= c && c <= 'Z')
        //     {
        //         c = (Char)(c | 0x20);
        //     }
        //     //if (c == '/' || c == '\\')
        //     //    c = '.';
        //     hash = (hash << 5) + hash + c;
        // }
        return hash;
        
        //***其他算法
        // ulong hash = 5381;
        // int len = str.Length;
        // int ser = 0;
        // /* variant with the hash unrolled eight times */ 
        // for (; len >= 8; len -= 8) { 
        //     hash = ((hash << 5) + hash) + str[ser++]; 
        //     hash = ((hash << 5) + hash) + str[ser++]; 
        //     hash = ((hash << 5) + hash) + str[ser++]; 
        //     hash = ((hash << 5) + hash) + str[ser++]; 
        //     hash = ((hash << 5) + hash) + str[ser++]; 
        //     hash = ((hash << 5) + hash) + str[ser++]; 
        //     hash = ((hash << 5) + hash) + str[ser++]; 
        //     hash = ((hash << 5) + hash) + str[ser++]; 
        // } 
        // switch (len) { 
        //     case 7: hash = ((hash << 5) + hash) + str[ser++]; break; 
        //     case 6: hash = ((hash << 5) + hash) + str[ser++]; break; 
        //     case 5: hash = ((hash << 5) + hash) + str[ser++]; break; 
        //     case 4: hash = ((hash << 5) + hash) + str[ser++]; break; 
        //     case 3: hash = ((hash << 5) + hash) + str[ser++]; break; 
        //     case 2: hash = ((hash << 5) + hash) + str[ser++]; break; 
        //     case 1: hash = ((hash << 5) + hash) + str[ser++]; break; 
        //     case 0: break; 
        // } 
        // GetInstance().hashDict.Add(str, hash);
        // return hash; 
    }

    
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
