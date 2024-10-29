using SimpleTable.Runtime;
using System;
using UnityEditor;
using UnityEngine;

namespace SimpleTable.Editor
{
    public abstract class TableEditor<SchemeT, KeyT> : UnityEditor.Editor
        where SchemeT : TableScheme<KeyT>, new()
    {
        public readonly GUILayoutOption REMOVE_BUTTON_WIDTH = GUILayout.Width(25);

        protected Table<SchemeT, KeyT> Table { get => target as Runtime.Table<SchemeT, KeyT>; }

        protected virtual string[] ColumNames { get; }

        protected string filter;

        public void OnEnable()
        {
            Table.showPage = 0;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            serializedObject.Update();

            using (var changeCheckingScope = new EditorGUI.ChangeCheckScope())
            {
                Table.itemPerPage = EditorGUILayout.IntField("Item Per Page", Table.itemPerPage);

                DrawFilter();

                DrawColumn();

                DrawBody();

                DrawNavigator();

                DrawOtherButtons();

                if (changeCheckingScope.changed)
                {
                    serializedObject.ApplyModifiedProperties();
                    EditorUtility.SetDirty(target);
                }
            }
        }

        private void DrawBody()
        {
            if (!IsValidFilter(filter))
            {
                DrawElements();
            }
            else
            {
                DrawFilterdElements();
            }
        }

        void DrawFilterdElements()
        {
            //모든 아이템을 순회하면서 필터링을 한다.
            for (int i = 0; i < Table.Count; ++i)
            {
                var item = Table.GetItem(i);
                if (IsFiltered(item))
                {
                    using (var horizontalScope = new EditorGUILayout.HorizontalScope())
                    {
                        var dataProperty = serializedObject.FindProperty("data");
                        var dataElementProperty = dataProperty.GetArrayElementAtIndex(i);
                        var keyElement = dataElementProperty.FindPropertyRelative("key");
                        EditorGUILayout.PropertyField(keyElement, GUIContent.none, false, null);
                        DrawItem(dataElementProperty);
                        if (GUILayout.Button("-", REMOVE_BUTTON_WIDTH))
                        {
                            //ask to confirm
                            if (EditorUtility.DisplayDialog("Remove", "Are you sure to remove this item?", "Yes", "No"))
                            {
                                Table.RemoveAt(i);
                                return;
                            }
                        }
                    }
                }
            }
        }

        void DrawElements()
        {
            int count = Table.Count;
           
            using (var verticalScope = new EditorGUILayout.VerticalScope())
            {
                for (int i = Table.showPage * Table.itemPerPage; i < count && i < (Table.showPage + 1) * Table.itemPerPage; ++i)
                {
                    using (var horizontalScope = new EditorGUILayout.HorizontalScope())
                    {
                        var dataProperty = serializedObject.FindProperty("data");
                        var dataElementProperty = dataProperty.GetArrayElementAtIndex(i);
                        var item = Table.GetItem(i);

                        var keyElement = dataElementProperty.FindPropertyRelative("key");
                        EditorGUILayout.PropertyField(keyElement, GUIContent.none, false, null);
                        DrawItem(dataElementProperty);
                        if (GUILayout.Button("-", REMOVE_BUTTON_WIDTH))
                        {
                            //ask to confirm
                            if (EditorUtility.DisplayDialog("Remove", "Are you sure to remove this item?", "Yes", "No"))
                            {
                                Table.RemoveAt(i);
                                return;
                            }
                        }
                    }
                }
            }

            //현재페이지 / 총페이지 labelfield로 보여주기
            EditorGUILayout.LabelField($"{Table.showPage + 1} / {(count - 1) / Table.itemPerPage + 1}");
        }
        private void DrawOtherButtons()
        {
            if (GUILayout.Button("+"))
            {
                Table.AddEmptyOne(Table.GetUniqueKey());
            }
            OnAfterAddButtonDrawing();

            //한번에 10개 추가하는 버튼
            if (GUILayout.Button("+10"))
            {
                for (int i = 0; i < 10; ++i)
                {
                    Table.AddEmptyOne(Table.GetUniqueKey());
                }
            }
            OnAfter10AddButtonDrawing();

            if (GUILayout.Button("Export to CSV"))
            {
                //export to csv
                ExportCSV();
            }
            OnAfterExportButtonDrawing();

            if (GUILayout.Button("Import from CSV"))
            {
                //import from csv
                ImportCSV();
            }
            OnAfterImportButtonDrawing();

            if (GUILayout.Button("Clear"))
            {
                //ask to confirm
                if (EditorUtility.DisplayDialog("Clear", "Are you sure to clear all items?", "Yes", "No"))
                {
                    Table.Clear();
                }
            }

            OnAfterClearButtonDrawing();
        }


        void ImportCSV()
        {
            throw new NotImplementedException();
        }

        void ExportCSV()
        {
            throw new NotImplementedException();
        }

        private void DrawNavigator()
        {
            if(IsValidFilter(filter))
            {
                return;
            }

            int count = Table.Count;
            //draw page naviator
            if (count > 0)
            {
                int pageCount = (count - 1) / Table.itemPerPage + 1;
                using (var horizontalScope = new EditorGUILayout.HorizontalScope())
                {
                    //한번에 페이지 10개를 이동하도록 하는 버튼
                    if(GUILayout.Button("<<"))
                    {
                        Table.showPage = Mathf.Max(0, Table.showPage - 10);
                    }

                    if (GUILayout.Button("<"))
                    {
                        Table.showPage = Mathf.Max(0, Table.showPage - 1);
                    }
                    
                    int startPage = Table.showPage / 10 * 10;
                    for (int i = startPage; i < Mathf.Min(pageCount, startPage + 10); ++i)
                    {
                        //현재 페이지는 빨간색으로한다.
                        if (i == Table.showPage)
                        {
                            GUI.color = Color.red;
                        }
                        if (GUILayout.Button((i + 1).ToString()))
                        {
                            Table.showPage = i;
                        }
                        GUI.color = Color.white;
                    }

                    if (GUILayout.Button(">"))
                    {
                        Table.showPage = Mathf.Min(pageCount - 1, Table.showPage + 1);
                    }

                    if (GUILayout.Button(">>"))
                    {
                        Table.showPage = Mathf.Min(pageCount - 1, Table.showPage + 10);
                    }
                }
            }
        }
        protected void DrawFilter()
        {
            filter = EditorGUILayout.TextField("Filter", filter);
        }

        protected void DrawColumn()
        {
            using(var horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Button("Key");
                for (int i = 0; i < ColumNames.Length; i++)
                {
                    GUILayout.Button(ColumNames[i]);
                }
                GUILayout.Button("", REMOVE_BUTTON_WIDTH);
            }
        }

        protected virtual bool IsFiltered(SchemeT item) { return true; }
        protected virtual void OnAfterImportButtonDrawing() { }
        protected virtual void OnAfterExportButtonDrawing() { }
        protected virtual void OnAfter10AddButtonDrawing() { }
        protected virtual void OnAfterAddButtonDrawing() { }
        protected virtual void OnAfterClearButtonDrawing() { }
        protected abstract void DrawItem(SerializedProperty elementProperty);
        protected abstract bool IsValidFilter(string filter);
    }
}
