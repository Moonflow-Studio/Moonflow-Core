using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Object = UnityEngine.Object;

public class MFLoadManager : MFSingleton<MFLoadManager>
{
    public Dictionary<string, int> hashDict;
    public Dictionary<int, MFAssetData> assetDict;
    public delegate void LoadCallback(Object asset);

    public static T Load<T>(string bundlePath, string name, LoadCallback cb = null) where T:UnityEngine.Object
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
            assetInfo.asset = AssetBundle.LoadFromFile(combinedpath);//+ ".assetbundle"

            assetInfo.refCount = 1;
            singleton.assetDict.Add(hash, assetInfo);
            if(cb!=null)cb(assetInfo.asset);
            return assetInfo.asset as T;
            
        }
    }

    public void RealLoadFromAssetBundle(string combinedPath)
    {
        
    }

    private static int GetHash(string str)
    {
        
        if (singleton.hashDict.TryGetValue(str, out int value))
        {
            return value;
        }

        int hash = str.GetHashCode();
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
}
