#if UNITY_EDITOR
using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;

using System.Collections.Generic;
using System.IO;

[InitializeOnLoad]
public class EditorSettingsSceneBuild : EditorWindow
{
    [MenuItem("Plugin Scenes/My Scenes")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EditorSettingsSceneBuild), false, "Scenes");
    }

    int selected; int changedSelected;
    List<string> scenes = new List<string>();

    void OnGUI()
    {
        EditorGUILayout.Space();

        scenes.Clear();

        string[] dropOptions = new string[EditorBuildSettings.scenes.Length];

        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];

            string sceneName = Path.GetFileNameWithoutExtension(scene.path);

            scenes.Add(sceneName);

            dropOptions[i] = scenes[i];

            if (scene.path == EditorSceneManager.GetActiveScene().path)
            {
                selected = changedSelected = i;
            }
        }

        changedSelected = EditorGUILayout.Popup(selected, dropOptions);

        if (selected != changedSelected)
        {
            selected = changedSelected;

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(EditorBuildSettings.scenes[changedSelected].path);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Scenes", EditorStyles.boldLabel);

        for (int i = 0; i < scenes.Count; i++)
        {
            if (GUILayout.Button(scenes[i], GUILayout.Height(25)))
            {
                selected = changedSelected = i;

                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path);
            }

            dropOptions[i] = scenes[i];
        }

        EditorGUILayout.LabelField("Scenes Build Settings", EditorStyles.boldLabel);

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Open Scene Build Settings", GUILayout.Height(25)))
        {
            GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
        }
    }
} 
#endif
