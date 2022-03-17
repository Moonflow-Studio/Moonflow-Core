using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFResInfo
{
    /// <summary>
    /// 资源名
    /// </summary>
    public string Name => _name;
    private string _name;
    /// <summary>
    /// 资源路径
    /// </summary>
    public string Path => _path;
    private string _path;
    // public Type type;

    public MFResInfo(string name, string path)
    {
        _name = name;
        _path = path;
    }
}
