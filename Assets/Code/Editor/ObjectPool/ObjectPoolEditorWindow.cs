using Database;
using ObjectPooling;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ObjectPoolEditorWindow : OdinMenuEditorWindow
    {
        public PoolCategoryData CurrentCategorySelected => MenuTree == null ? null : MenuTree.Selection.SelectedValue as PoolCategoryData;

        [MenuItem("Tools/ObjectPool Editor")]

        public static void OpenWindow()
        {
            ObjectPoolEditorWindow window = GetWindow<ObjectPoolEditorWindow>(string.Format("ObjectPool Editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }


        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTreeDrawingConfig style = new OdinMenuTreeDrawingConfig();
            OdinMenuTree tree = new OdinMenuTree(false, style);

            List<PoolCategoryData> allObjectPoolCategories = new List<PoolCategoryData>(MainDatabases.Instance.ObjectPoolDatabase.PoolCategories);
            allObjectPoolCategories.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));

            for (int i = 0; i < allObjectPoolCategories.Count; i++)
            {
                tree.AddObjectAtPath(i + " - " + allObjectPoolCategories[i].CategoryName, allObjectPoolCategories[i]);
            }

            return tree;
        }


        protected override void OnImGUI()
        {
            base.OnImGUI();

            EditorGUILayout.LabelField("ToolBox", SirenixGUIStyles.BoldLabelCentered);
            SirenixEditorGUI.HorizontalLineSeparator(Color.white, 2);
            SirenixEditorGUI.BeginHorizontalToolbar();

            if (GUILayout.Button("AddNewCategory", SirenixGUIStyles.Button))
            {
                AddNewCategory();
            }

            if (GUILayout.Button("DeleteCategory", SirenixGUIStyles.Button))
            {
                DeleteCategory();
            }


            SirenixEditorGUI.EndHorizontalToolbar();

            SirenixEditorGUI.BeginHorizontalToolbar();

            if (GUILayout.Button("Refresh", SirenixGUIStyles.Button))
            {
                ForceMenuTreeRebuild();
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Debug.Log("ObjectPool Saving");
            SaveThisAsset();
        }

        private void SaveThisAsset()
        {
            if (EditorApplication.isUpdating)
            {
                return;
            }

            EditorUtility.SetDirty(MainDatabases.Instance.ObjectPoolDatabase);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void AddNewCategory()
        {
            MainDatabases.Instance.ObjectPoolDatabase.AddCategory("NewCategory");

            ForceMenuTreeRebuild();
        }

        private void DeleteCategory()
        {
            if (CurrentCategorySelected != null)
            {
                if (EditorUtility.DisplayDialog("Are you sure?", $"Delete this category? - {CurrentCategorySelected.CategoryName}", "Yes", "Cancel") == false)
                    return;

                MainDatabases.Instance.ObjectPoolDatabase.RemoveCategory(CurrentCategorySelected);
                ForceMenuTreeRebuild();
            }
        }
    }
}
