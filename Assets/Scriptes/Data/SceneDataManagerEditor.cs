using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneDataManager))]
public class SceneDataManagerEditor : Editor {
    private SceneDataManager sceneDataManager;
    private string levelName;
    private string levelIndex;

    private void OnEnable() {
        sceneDataManager = (SceneDataManager)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GUILayout.Space(25);

        levelIndex = GUILayout.TextField(levelIndex);
        if (GUILayout.Button("Load")) {
            if (int.TryParse(levelIndex, out int result))
                sceneDataManager.Load(result);
            else
                Debug.Log(levelIndex + " isnt an int");
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Clean")) {
            sceneDataManager.Clean();
        }

        GUILayout.Space(10);

        levelName = GUILayout.TextField(levelName);
        if (GUILayout.Button("Save")) {
            sceneDataManager.Save(levelName);
        }
    }
}
