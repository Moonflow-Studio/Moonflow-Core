using System;
using UnityEditor;
using UnityEngine;

namespace Moonflow
{
    public struct MFRangeValue<T>
    {
        public T current;
        public T max;
    }

    [CustomPropertyDrawer(typeof(MFRangeValue<float>))]
    public class MFRangeFloatDrawer: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //重载BeginProperty
            EditorGUI.BeginProperty(position,label,property);  

            //绘制标签
            position = EditorGUI.PrefixLabel(position,GUIUtility.GetControlID(FocusType.Passive),label);
        
            //Unity默认的每个属性字段都会占用一行，我们这里希望一条自定义Property占一行
            //要是实现这个要求我们分三步： 1. 取消缩进  2. 设置PropertyField 3.还原缩进
        
            //不要缩进子字段，只有取消了缩进，Rect挤才一行才不会混乱
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //计算要用到的属性显示rect   Rect(x,y,width,height)x,y是左顶点
            var current = new Rect(position.x,position.y,30,position.height);
            var max = new Rect(position.x + 35,position.y,50,position.height);

            //绘制字段 - 将GUIContent.none传递给每个字段，以便绘制它们而不是用标签
            //属性绘制器不支持布局来创建GUI;
            //因此，您必须使用的类是EditorGUI而不是EditorGUILayout。这就是为什么要给每个属性指定Rect
            EditorGUI.PropertyField(current,property.FindPropertyRelative("current"),GUIContent.none);
            EditorGUI.PropertyField(max,property.FindPropertyRelative("max"),GUIContent.none);

            //将缩进还原，好习惯
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();        
        }
    }
    
    [CustomPropertyDrawer(typeof(MFRangeValue<int>))]
    public class MFRangeIntDrawer: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //重载BeginProperty
            EditorGUI.BeginProperty(position,label,property);  

            //绘制标签
            position = EditorGUI.PrefixLabel(position,GUIUtility.GetControlID(FocusType.Passive),label);
        
            //Unity默认的每个属性字段都会占用一行，我们这里希望一条自定义Property占一行
            //要是实现这个要求我们分三步： 1. 取消缩进  2. 设置PropertyField 3.还原缩进
        
            //不要缩进子字段，只有取消了缩进，Rect挤才一行才不会混乱
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //计算要用到的属性显示rect   Rect(x,y,width,height)x,y是左顶点
            var current = new Rect(position.x,position.y,30,position.height);
            var max = new Rect(position.x + 35,position.y,50,position.height);

            //绘制字段 - 将GUIContent.none传递给每个字段，以便绘制它们而不是用标签
            //属性绘制器不支持布局来创建GUI;
            //因此，您必须使用的类是EditorGUI而不是EditorGUILayout。这就是为什么要给每个属性指定Rect
            EditorGUI.PropertyField(current,property.FindPropertyRelative("current"),GUIContent.none);
            EditorGUI.PropertyField(max,property.FindPropertyRelative("max"),GUIContent.none);

            //将缩进还原，好习惯
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();        
        }
    }
}