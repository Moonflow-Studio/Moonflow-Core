using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MoonflowCore.Runtime.Framework.Data;
using UnityEngine;
using Object = UnityEngine.Object;

public class MFLoadManager : MFSingleton<MFLoadManager>, IMFSceneRefresh
{
    public Dictionary<string, int> hashDict;
    public Dictionary<int, MFAssetData> assetDict;
    public Dictionary<string, AssetBundle> loadedBundle;
    public delegate void LoadCallback(Object asset);

    public override MFLoadManager GetInstance()
    {
        var instance = base.GetInstance();
        instance.hashDict = new Dictionary<string, int>();
        instance.assetDict = new Dictionary<int, MFAssetData>();
        instance.loadedBundle = new Dictionary<string, AssetBundle>();
        return base.GetInstance();
    }

    public static T Load<T>(string bundlePath, string name, bool immediate = false, LoadCallback cb = null) where T:UnityEngine.Object
    {
        string combinedpath = bundlePath + name;
        int hash = GetHash(combinedpath);
        if (singleton.assetDict.TryGetValue(hash, out MFAssetData data))
        {
            data.refCount++;
            singleton.assetDict[hash] = data;
            return data.asset as T;
        }
        else
        {
            MFAssetData assetInfo = new MFAssetData()
            {
                path = bundlePath,
                name = name,
                hash = hash,
            };
            //TODO:异步加载asset
            if (immediate)
            {
                RealLoad(bundlePath, name, assetInfo);
            }
            else
            {
                RealLoadAsync(bundlePath, name, assetInfo);
            }

            cb?.Invoke(assetInfo.asset);
            assetInfo.refCount = 1;
            singleton.assetDict.Add(hash, assetInfo);
            return assetInfo.asset as T;
        }
    }

    private static void RealLoad(string bundlePath, string name, MFAssetData assetInfo)
    {
        singleton.loadedBundle.TryGetValue(GetStreamingAssetsPath() + bundlePath, out AssetBundle bundle);
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(GetStreamingAssetsPath() + bundlePath);
            singleton.loadedBundle.Add(bundlePath, bundle);
        }
        assetInfo.asset = bundle.LoadAsset(name);
    }

    private static void RealLoadAsync(string bundlePath, string name, MFAssetData assetInfo)
    {
        singleton.loadedBundle.TryGetValue(GetStreamingAssetsPath() + bundlePath, out AssetBundle bundle);
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
                singleton.loadedBundle.Add(bundlePath, bundle);
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

    public void OnEnterScene()
    {
        // throw new NotImplementedException();
    }

    public void OnLeaveScene()
    {
        UnloadBundles();
    }
    private static void UnloadBundles()
    {
        foreach (var bundlePair in singleton.loadedBundle)
        {
            bundlePair.Value.Unload(false);
        }
        singleton.loadedBundle.Clear();
    }


    private static int GetHash(string str)
    {
        
        if (singleton.hashDict.TryGetValue(str, out int value))
        {
            return value;
        }

        //C#原生hash算法
        int hash = str.GetHashCode();
        
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
        singleton.hashDict.Add(str, hash);
        return hash; 
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
