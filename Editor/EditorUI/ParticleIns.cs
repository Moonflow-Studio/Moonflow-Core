using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ParticleIns : ShaderGUI
{
    public enum BlendMode
    {
        AlphaBlend,
        Additive,
        Subtractive,
        Modulate,
        None
    }
    
    /*public enum CompareFunction
    {
        Less,
        Greater,
        LEqual,
        GEqual,
        Equal,
        NotEqual,
        Always
    }*/

    static GUIContent[] blendNames = Array.ConvertAll(Enum.GetNames(typeof(BlendMode)), item => new GUIContent(item));
    MaterialProperty blendMode = null;
    static GUIContent[] zTestModeNames = Array.ConvertAll(Enum.GetNames(typeof(CompareFunction)), item1 => new GUIContent(item1));
    MaterialProperty zTestMode = null;
    MaterialEditor materialEditor;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        Material material = materialEditor.target as Material;
        this.materialEditor = materialEditor;
        
        EditorGUI.BeginChangeCheck();
        blendMode = FindProperty("_Mode", props);
        BlendMode bm = (BlendMode)blendMode.floatValue;
        if (blendMode != null)
        {
            BlendModePopup(material, ref bm);
        }
        
        zTestMode = FindProperty("_ZTest", props);
        CompareFunction zm = (CompareFunction) zTestMode.floatValue;
        if (zTestMode != null)
        {
            ZTestPopup(material, ref zm);
        }
        
        
        base.OnGUI(materialEditor, props);
        
        if (EditorGUI.EndChangeCheck())
        {
            if (blendMode != null)
            {
                materialEditor.RegisterPropertyChangeUndo("Blend Mode");
                blendMode.floatValue = (float)bm;
            }
            if (zTestMode != null)
            {
                materialEditor.RegisterPropertyChangeUndo("ZTest Mode");
                zTestMode.floatValue = (float)zm;
            }
        }
    }

    public void FindProperties(MaterialProperty[] props)
    {
    }

    void BlendModePopup(Material material, ref BlendMode mode)
    {
        mode = (BlendMode)blendMode.floatValue;

        mode = (BlendMode)EditorGUILayout.Popup(EditorGUIUtility.TrTextContent("Blend Mode"), (int)mode, blendNames);
        switch ((BlendMode)material.GetFloat("_Mode"))
        {   
            case BlendMode.AlphaBlend:
                //material.DisableKeyword("_ADD_BLEND");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                break;                
            case BlendMode.Additive:
                //material.EnableKeyword("_ADD_BLEND");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
                break;
            case BlendMode.Subtractive:
                //material.EnableKeyword("_ADD_BLEND");
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
                break;
            case BlendMode.Modulate:
                //material.DisableKeyword("_ADD_BLEND");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                break;
            case BlendMode.None:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                break;
        }
    }    
    
    void ZTestPopup(Material material, ref CompareFunction mode)
    {
        mode = (CompareFunction)zTestMode.floatValue;
        mode = (CompareFunction)EditorGUILayout.Popup(EditorGUIUtility.TrTextContent("ZTest Mode"), (int)mode, zTestModeNames);
        material.SetInt("_ZTest", (int) mode);
    } 
    
}