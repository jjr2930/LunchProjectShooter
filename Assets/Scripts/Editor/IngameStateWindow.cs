using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IngameStateWindow : EditorWindow
{
    [MenuItem("Tools/Ingame State Window")]
    public static void OpenWindow()
    {
        var window = GetWindow<IngameStateWindow>(true, "Ingame State");
        window.titleContent = new GUIContent("Ingame State");
    }

    public void OnGUI()
    {
        if(false == Application.isPlaying)
        {
            EditorGUILayout.HelpBox("This window is only available in play mode.", MessageType.Info);
            return;
        }

        using(var verticalScope = new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.LabelField(IngameState.HostIpToJoin);
        }
    }
}
