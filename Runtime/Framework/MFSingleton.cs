using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 抽象单例基类
/// </summary>
public abstract class MFSingleton
{
}

/// <summary>
/// 单例模板
/// </summary>
/// <typeparam name="T">新建的单例</typeparam>
public abstract class MFSingleton<T>: MFSingleton where T:new()
{
    /// <summary>
    /// 目标单例
    /// </summary>
    protected static T singleton;
    /// <summary>
    /// 获得单例
    /// </summary>
    /// <returns>返回单例</returns>
    public static T GetInstance()
    {
        if (singleton == null) singleton = new T();
        return singleton;
    }
}
