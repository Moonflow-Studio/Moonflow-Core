using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MFSingleton
{
}

public abstract class MFSingleton<T>: MFSingleton where T:new()
{
    protected static T singleton;
    public static T GetInstance()
    {
        if (singleton == null) singleton = new T();
        return singleton;
    }
}
