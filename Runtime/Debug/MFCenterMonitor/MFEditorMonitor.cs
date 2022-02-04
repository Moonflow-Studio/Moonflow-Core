using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MFEditorMonitor
{
    public abstract void DrawMonitor();
    public abstract void Initial();
    public abstract string GetName();
    public abstract void OnValidate();
}
public abstract class MFEditorMonitor<T>:MFEditorMonitor where T:MFSingleton
{
    protected readonly GUIStyle SystemTitleStyle = new GUIStyle()
    {
        fontStyle = FontStyle.Bold, 
        fontSize = 20,
        normal = new GUIStyleState()
        {
            textColor = Color.white
        }
    };
    protected readonly GUIStyle TableTitleStyle = new GUIStyle()
    {
        fontStyle = FontStyle.Bold,
        fontSize = 14,
        normal = new GUIStyleState()
        {
            textColor = Color.black
        }
    };
    public T target;
    
}
