using System;
using System.Collections;
using System.Collections.Generic;
using MoonflowCore.Runtime.Framework.Data;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 资产Runtime管理类
/// </summary>
public class MFAssetData:IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    public MFResInfo resInfo;
    /// <summary>
    /// 资源hash
    /// </summary>
    public int hash;
    /// <summary>
    /// 资源当前Flag状态
    /// </summary>
    public MFFlag flag;
    /// <summary>
    /// 资源引用计数
    /// </summary>
    public int refCount;
    /// <summary>
    /// 资源实例
    /// </summary>
    public UnityEngine.Object asset;


    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    ~MFAssetData()
    {
        Object.Destroy(asset);
    }
}
