using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 监视器抽象基类
/// </summary>
public abstract class MFEditorMonitor
{
    /// <summary>
    /// 监视器在面板中如何绘制
    /// </summary>
    public abstract void DrawMonitor();
    /// <summary>
    /// 监视器初始化
    /// </summary>
    public abstract void Initial();
    /// <summary>
    /// 监视器监视的类名
    /// </summary>
    /// <returns>返回类名</returns>
    public abstract string GetName();
    /// <summary>
    /// 监视器面板上数据修改时的操作
    /// </summary>
    public abstract void OnValidate();
}
/// <summary>
/// 单例的Editor观测类
/// </summary>
/// <typeparam name="T">被观测的单例</typeparam>
public abstract class MFEditorMonitor<T>:MFEditorMonitor where T:MFSingleton
{
    /// <summary>
    /// 被监视系统标题类型
    /// </summary>
    protected readonly GUIStyle SystemTitleStyle = new GUIStyle()
    {
        fontStyle = FontStyle.Bold, 
        fontSize = 20,
        normal = new GUIStyleState()
        {
            textColor = Color.white
        }
    };
    /// <summary>
    /// 被监视表格标题类型
    /// </summary>
    protected readonly GUIStyle TableTitleStyle = new GUIStyle()
    {
        fontStyle = FontStyle.Bold,
        fontSize = 14,
        normal = new GUIStyleState()
        {
            textColor = Color.black
        }
    };
    /// <summary>
    /// 被监视的系统单例
    /// </summary>
    public T target;
    
}
