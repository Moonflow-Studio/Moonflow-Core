using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Moonflow.Core
{
    public class MFCenterMonitorExplorer : EditorWindow
    {
        private int selected;
        
        // private MFUberPostFeature _uberPost;
        private List<MFEditorMonitor> systemList;
        private List<string> systemName;
        private GUIStyle _tableTitleStyle;
        private bool _initialized = false;
        [MenuItem("Moonflow/控制中心")]
        private static void ShowWindow()
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(MFCenterMonitorExplorer));
            window.Show();
            window.titleContent = new GUIContent("Moonflow控制中心");
        }
    
        private void OnEnable()
        {
            GetAllInstance();
            // _uberPost = MFUberPostFeature.GetInstance();
        }
    
        private void OnDisable()
        {
            _initialized = false;
        }
    
        private void OnValidate()
        {
            if (systemList == null) return;
            foreach (var system in systemList)
            {
                system.OnValidate();
            }
        }
    
        private void GetAllInstance()
        {
            systemList = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(item =>
                    item.GetTypes()
                )
                .Where(item => item.IsClass && typeof(MFEditorMonitor).IsAssignableFrom(item) && item.GetConstructor(Type.EmptyTypes)!=null)
                .Select(type => (MFEditorMonitor) Activator.CreateInstance(type)).ToList();
            systemName = new List<string>();
            for (int i = 0; i < systemList.Count; i++)
            {
                systemList[i].Initial();
                systemName.Add(systemList[i].GetName());
            }
            _initialized = true;
        }
        private void OnGUI()
        {
            if (!_initialized) GetAllInstance();
    
            if (systemName.Count > 0)
            {
                selected = GUILayout.SelectionGrid(selected, systemName.ToArray(), systemList.Count);
                systemList[selected].DrawMonitor();
            }
            EditorGUILayout.Space(10);
        }
    
        
    }

}
