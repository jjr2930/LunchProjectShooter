using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SimpleTable.Editor
{
    public class TableGeneratorWindow : EditorWindow
    {
        const string SCHEME_NAME_KEYWORD = "{SCHEME_NAME}";
        const string SCHEME_FIELD_HERE_KEYWORD = "{SCHEME_FIELD_HERE}";
        const string SCHEME_KEY_TYPE_KEYWORD = "{KEY_TYPE}";
        const string TABLE_FILE_NAME_KEYWORD = "{TABLE_NAME}";
        const string TABLE_MENU_NAME_KEYWORD = "{TABLE_MENU_NAME}";
        const string TABLE_DRAW_ITEM_KEYWORD = "{TABLE_DRAW_ITEM}";
        const string FIELD_NAME_KEYWORD = "{FIELD_NAME}";
        const string COLUMN_NAME_KEYWORD = "{COLUMN_NAME}";
        const string TABLE_TEMPLATE = @"

using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
using SimpleTable.Editor;
#endif

using SimpleTable.Runtime;

[Serializable]
public class {SCHEME_NAME}Scheme: TableScheme<{KEY_TYPE}>
{
    {SCHEME_FIELD_HERE}
}

[CreateAssetMenu(fileName = ""{TABLE_NAME}"", menuName = ""Tables/{TABLE_MENU_NAME}"")]
public class {TABLE_NAME} : Table<{SCHEME_NAME}Scheme,{KEY_TYPE}>
{
    public override int GetUniqueKey()
    {
        throw new NotImplementedException();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof({TABLE_NAME}))]
public class {TABLE_NAME}Editor : TableEditor<{SCHEME_NAME}Scheme, {KEY_TYPE}>
{
    protected override string[] ColumNames => {COLUMN_NAME};
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    protected override void DrawItem(SerializedProperty dataElementProperty)
    {
        {TABLE_DRAW_ITEM}
    }

    protected override bool IsFiltered({SCHEME_NAME}Scheme item)
    {
        throw new NotImplementedException();
    }

    protected override bool IsValidFilter(string filter)
    {
        throw new NotImplementedException();
    }
}
#endif
";

        static readonly string PROPERTY_FIELD_TEMPALTE = @"
var {FIELD_NAME}Element = dataElementProperty.FindPropertyRelative(""{FIELD_NAME}"");
EditorGUILayout.PropertyField({FIELD_NAME}Element, GUIContent.none, false, null);
";
        [MenuItem("Tools/Table Generator")]
        public static void ShowWindow()
        {
            var window = GetWindow<TableGeneratorWindow>();
            window.titleContent = new GUIContent("Table Generator");
            window.Show();
        }

        static readonly Type[] suppportedTypes = new Type[]
        {
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(string),
            typeof(bool),
            typeof(Vector2),
            typeof(Vector3),
            typeof(Vector4),
            typeof(Vector2Int),
            typeof(Vector3Int),
            typeof(Quaternion),
            typeof(Color),
            typeof(Rect),
            typeof(Bounds),
            typeof(LayerMask),
            typeof(AnimationCurve),
            typeof(Gradient),
            typeof(GameObject),
            typeof(Transform),
            //added if you want to support more types
        };

        public class FieldInfo
        {
            public string name;
            public Type type;

            public FieldInfo(string name, Type type)
            {
                this.name = name;
                this.type = type;
            }
        }

        static string tableName = "NewTableName";
        static string tableMenuName = "NewTableName";
        static string tableSchemeName = "NewSchemeName";
        static int keyTypeSelectedIndex = 0;
        static string sourceCodeText = "";
        static List<FieldInfo> schemeFields = new List<FieldInfo>();
        static Vector2 scrollPosition;

        private void OnGUI()
        {
            using (var verticalScope = new EditorGUILayout.VerticalScope())
            {
                using (var changeScope = new EditorGUI.ChangeCheckScope())
                {
                    tableName = EditorGUILayout.TextField("Table Name", tableName);
                    tableMenuName = EditorGUILayout.TextField("Table Menu Name", tableMenuName);
                    tableSchemeName = EditorGUILayout.TextField("Scheme Name", tableSchemeName);
                    keyTypeSelectedIndex = EditorGUILayout.Popup("Key Type", keyTypeSelectedIndex, suppportedTypes.Select(x => x.Name).ToArray());

                    //draw scheme fields
                    for (int i = 0; i < schemeFields.Count; ++i)
                    {
                        using (var horizontalScope = new EditorGUILayout.HorizontalScope())
                        {
                            var field = schemeFields[i];
                            field.name = EditorGUILayout.TextField(field.name);
                            field.type = suppportedTypes[EditorGUILayout.Popup(Array.IndexOf(suppportedTypes, field.type), suppportedTypes.Select(x => x.Name).ToArray())];
                            if(GUILayout.Button("-"))
                            {
                                //ask confirm and remove
                                if (EditorUtility.DisplayDialog("Remove Field", "Do you want to remove this field?", "Yes", "No"))
                                {
                                    schemeFields.RemoveAt(i);
                                    RefreshSourceCode();
                                    return;
                                }
                            }
                        }
                    }

                    if (GUILayout.Button("Add Field"))
                    {
                        schemeFields.Add(new FieldInfo("NewField", suppportedTypes[0]));
                    }

                    if (changeScope.changed)
                    {
                        RefreshSourceCode();
                    }
                }

                if (GUILayout.Button("Save"))
                {
                    var path = EditorUtility.SaveFilePanel("Save Table Script", Application.dataPath, tableName, "cs");
                    if (!string.IsNullOrEmpty(path))
                    {
                        System.IO.File.WriteAllText(path, sourceCodeText);
                        AssetDatabase.Refresh();
                    }
                }

                using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPosition))
                {
                    GUI.enabled = false;
                    {
                        EditorGUILayout.TextArea(sourceCodeText);
                    }
                    GUI.enabled = true;

                    scrollPosition = scroll.scrollPosition;
                }
            }
        }

        private void RefreshSourceCode()
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(tableMenuName) || string.IsNullOrEmpty(tableSchemeName))
            {
                Debug.LogError("Table Name, Table Menu Name, Scheme Name must not be empty");
                return;
            }

            var drawItem = PROPERTY_FIELD_TEMPALTE.Replace(FIELD_NAME_KEYWORD, "key");
            foreach (var field in schemeFields)
            {
                drawItem += PROPERTY_FIELD_TEMPALTE.Replace(FIELD_NAME_KEYWORD, field.name);
            }

            var schemeFieldHere = "";
            foreach (var field in schemeFields)
            {
                schemeFieldHere += $"public {field.type.FullName} {field.name};\n";
            }

            sourceCodeText = TABLE_TEMPLATE
                .Replace(COLUMN_NAME_KEYWORD, $"new string[]{{\"key\",{string.Join(",", schemeFields.Select(x => $"\"{x.name}\""))}}}")
                .Replace(SCHEME_NAME_KEYWORD, tableSchemeName)
                .Replace(SCHEME_FIELD_HERE_KEYWORD, schemeFieldHere)
                .Replace(SCHEME_KEY_TYPE_KEYWORD, suppportedTypes[keyTypeSelectedIndex].FullName)
                .Replace(TABLE_FILE_NAME_KEYWORD, tableName)
                .Replace(TABLE_MENU_NAME_KEYWORD, tableMenuName)
                .Replace(TABLE_DRAW_ITEM_KEYWORD, drawItem);
        }
    }
}
