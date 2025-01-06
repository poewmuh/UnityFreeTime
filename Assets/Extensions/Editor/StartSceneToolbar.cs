using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace Extensions
{
    [InitializeOnLoad]
    public static class StartSceneToolbar
    {
        private const string globalScenesPath = "Assets/1GameFolder/Scenes/";
        private const string menuScenePath = globalScenesPath + "MainMenu.unity";

        private static SceneAsset startScene
        {
            get
            {
                const string path = globalScenesPath + "Entry.unity";
                return AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            }
        }

        static StartSceneToolbar()
        {
            ToolbarExtender.LeftToolbarGUI.Remove(OnToolbarGUI);
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);

            EditorApplication.playModeStateChanged += ResetStartScene;
        }

        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(new GUIContent($"{startScene.name}", $"Start {startScene.name} Scene"),
                    EditorStyles.toolbarButton))
            {
                EditorSceneManager.playModeStartScene = startScene;
                EditorApplication.isPlaying = true;
            }
            
            if (GUILayout.Button(new GUIContent($"Menu"),
                    EditorStyles.toolbarButton))
            {
                EditorSceneManager.OpenScene(menuScenePath);
            }
        }

        private static void ResetStartScene(PlayModeStateChange state)
        {
            if (EditorSceneManager.playModeStartScene != startScene ||
                state != PlayModeStateChange.ExitingPlayMode) return;

            EditorSceneManager.playModeStartScene = null;
        }
    }
}