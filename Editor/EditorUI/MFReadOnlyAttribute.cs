using UnityEditor;
using UnityEngine;

namespace Moonflow.Utility
{
    /// <summary>
    /// 面板只读Attribute
    /// </summary>
    public class MFReadOnlyAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// 面板绘制
    /// </summary>
    [CustomPropertyDrawer(typeof(MFReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        /// <summary>
        /// 用来保持原有高度
        /// </summary>
        /// <param name="property">被操作的Property</param>
        /// <param name="label">Property名字</param>
        /// <returns></returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}