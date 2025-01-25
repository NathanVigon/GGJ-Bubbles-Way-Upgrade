using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class SceneDataManager : MonoBehaviour {
    [Header("Game")]
    public static SceneDataManager Instance;
    [SerializeField] private GameData GameData;
    public LevelData ActualLevelData { get; private set; }

    [Header("Parent")]
    [SerializeField] private Transform StartPointParent;
    [SerializeField] private Transform EndPointParent;
    [SerializeField] private Transform ObstacleParent;

    [Header("LevelData")]
    [SerializeField] private GameObject[] BulleDispoToSave;
    [SerializeField] private int MoneyToSave;
    [SerializeField] private int TailleXToSave;
    [SerializeField] private int TailleYToSave;
    [SerializeField] private int nbrBonhommeToSave;
    [SerializeField] private int[] nbrPointEtoileToSave;

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    #region LOAD

    public void Load(int levelIndex) {
        Clean();
        if (levelIndex < GameData.levelDatas.Count) {
            ActualLevelData = GameData.levelDatas[levelIndex];
            ActualLevelData.LoadLevel(StartPointParent, EndPointParent, ObstacleParent);
            //TODO load grille + reste de l'ui (money, bulle dispo, etc)
        } else
            Debug.Log(levelIndex + " est hors limite");
    }

    #endregion

    #region SAVE

    public void Save(string levelName) {
        if (TailleXToSave % 2 != 0 || TailleYToSave % 2 != 0) {
            Debug.Log("Taille de la grille impaire");
            return;
        }

        LevelData levelData = ScriptableObject.CreateInstance<LevelData>();

        levelData.StartPoint = SaveStartOrEndPoint(StartPointParent);
        levelData.EndPoints = SaveTabEntity(EndPointParent);
        levelData.Obstacles = SaveTabEntity(ObstacleParent);

        levelData.Money = MoneyToSave;
        levelData.TailleX = TailleXToSave;
        levelData.TailleY = TailleYToSave;
        levelData.NbrBonhomme = nbrBonhommeToSave;
        levelData.NbrPointEtoile = nbrPointEtoileToSave;

        GameData.levelDatas.Add(levelData);

        AssetDatabase.CreateAsset(levelData, "Assets/ScriptableObjects/LevelsData/" + levelName + ".asset");
    }

    private EntityData[] SaveTabEntity(Transform parent) {
        var entitys = new EntityData[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++) {
            entitys[i] = new EntityData() {
                prefab = parent.transform.GetChild(i).gameObject,
                position = parent.transform.GetChild(i).position,
                rotation = parent.transform.GetChild(i).rotation,
                scale = parent.transform.GetChild(i).localScale
            };
        }
        return entitys;
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
