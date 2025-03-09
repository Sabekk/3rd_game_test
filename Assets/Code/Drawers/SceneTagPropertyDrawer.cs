using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Core.Drawer
{
    public class SceneTagPropertyDrawer : OdinAttributeDrawer<SceneTagAttribute, string>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Rect editorRect = EditorGUILayout.GetControlRect();
            string[] sceneNames = new string[EditorSceneManager.sceneCountInBuildSettings];

            for (int i = 0; i < EditorSceneManager.sceneCountInBuildSettings; i++)
            {
                var definition = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
                sceneNames[i] = definition;
            }

            this.ValueEntry.SmartValue = SirenixEditorFields.Dropdown(editorRect, label, this.ValueEntry.SmartValue, sceneNames);
        }
    }
}