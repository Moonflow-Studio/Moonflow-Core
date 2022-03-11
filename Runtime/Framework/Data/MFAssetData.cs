using System.Collections;
using System.Collections.Generic;
using MoonflowCore.Runtime.Framework.Data;
using UnityEngine;

/// <summary>
/// 资产Runtime管理类
/// </summary>
public class MFAssetData
{
    public string name;
    public string path;
    public int hash;
    public MFFlag flag;
    public int refCount;
    public UnityEngine.Object asset;
    
}
