using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class SceneDataManager : MonoBehaviour {
    public static SceneDataManager Instance;
    [SerializeField] private GameData GameData;

    [SerializeField] private Transform StartPointParent;
    [SerializeField] private Transform EndPointParent;
    [SerializeField] private Transform ObstacleParent;

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    #region LOAD

    public void Load(int levelIndex) {
        Clean();
        if (levelIndex < GameData.levelDatas.Count)
            GameData.levelDatas[levelIndex].LoadLevel(StartPointParent, EndPointParent, ObstacleParent);
        else
            Debug.Log(levelIndex + " est hors limite");
    }

    #endregion

    #region SAVE

    public void Save(string levelName) {
        LevelData levelData = ScriptableObject.CreateInstance<LevelData>();

        levelData.StartPoint = SaveStartOrEndPoint(StartPointParent);
        levelData.EndPoint = SaveStartOrEndPoint(EndPointParent);
        levelData.Obstacles = SaveObstacle(ObstacleParent);
        GameData.levelDatas.Add(levelData);

        AssetDatabase.CreateAsset(levelData, "Assets/ScriptableObjects/LevelsData/" + levelName + ".asset");
    }

    private EntityData[] SaveObstacle(Transform parent) {
        var obstacles = new EntityData[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++) {
            obstacles[i] = new EntityData() {
                prefab = parent.transform.GetChild(i).gameObject,
                position = parent.transform.GetChild(i).position,
                rotation = parent.transform.GetChild(i).rotation,
                scale = parent.transform.GetChild(i).localScale
            };
        }
        return obstacles;
    }

    private EntityData SaveStartOrEndPoint(Transform parent) {
        return new EntityData() {
            prefab = parent.transform.GetChild(0).gameObject,
            position = parent.transform.GetChild(0).position,
            rotation = parent.transform.GetChild(0).rotation,
            scale = parent.transform.GetChild(0).localScale
        };
    }

    #endregion

    #region CLEAN

    public void Clean() {
        DestroyChilds(StartPointParent);
        DestroyChilds(EndPointParent);
        DestroyChilds(ObstacleParent);
    }
    private void DestroyChilds(Transform parent) {
        for (int i = 0; i < parent.transform.childCount; i++) {
            if (Application.isEditor) {
                DestroyImmediate(parent.transform.GetChild(i).gameObject);
                i--;
            } else
                Destroy(parent.transform.GetChild(i).gameObject);
        }
    }

    #endregion
}
