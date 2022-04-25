using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Moonflow
{
   /// <summary>
   /// 编辑器界面拓展
   /// </summary>
   public static class MFEditorUI
   {
      /// <summary>
      /// 分页列表
      /// </summary>
      /// <param name="objects">被操作列表</param>
      /// <param name="index">当前序号</param>
      /// <param name="foldout">展开状态</param>
      /// <param name="pageNumber">单页显示数量</param>
      /// <typeparam name="T">被操作列表的类型</typeparam>
      public static void DrawFlipList<T>(List<T> objects, ref int index, ref bool foldout, int pageNumber) where T:Object
      {
         using (new EditorGUILayout.HorizontalScope())
         {
            int count = objects?.Count ?? 0;
            foldout = EditorGUILayout.Foldout(foldout, "Object");
            EditorGUILayout.LabelField($"{index * pageNumber}-{index * pageNumber + pageNumber}/{count}");
            if (GUILayout.Button("<-"))
            {
               index--;
               if (index < 0) index = 0;
            }

            if (GUILayout.Button("->"))
            {
               index++;
               if (index > count / pageNumber) index = count / pageNumber;
            }
         }

         if (foldout && objects != null)
         {
            for (int i = index * pageNumber; i < Mathf.Min(index * pageNumber + pageNumber, objects.Count); i++)
            {
               EditorGUILayout.ObjectField($"Target{i}", objects[i], typeof(T), true);
            }
         }
      }
   }
}
