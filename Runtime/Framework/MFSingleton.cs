using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MFSingleton
{
}

/// <summary>
/// 单例模板
/// </summary>
/// <typeparam name="T">新建的单例</typeparam>
public abstract class MFSingleton<T>: MFSingleton where T:new()
{
    protected static T singleton;
    public static T GetInstance()
    {
        if (singleton == null) singleton = new T();
        return singleton;
    }
}
