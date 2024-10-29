using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using JYSimpleGrid.Runtime;

namespace JYSimpleGrid.Editor
{
    [CustomEditor(typeof(MonoGrid))]
    public class MonoGridEditor : UnityEditor.Editor
    {
        MonoGrid Script { get { return target as MonoGrid; } }

        public override void OnInspectorGUI()
        {
            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();

                if (changeScope.changed)
                {
                    Script.RefreshGrid();
                }
            }            
        }   
    }
}
