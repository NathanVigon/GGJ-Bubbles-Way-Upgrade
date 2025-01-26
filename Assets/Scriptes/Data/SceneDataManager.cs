using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class SceneDataManager : MonoBehaviour {
    [Header("Game")]
    public static SceneDataManager Instance;
    [SerializeField] private GameData GameData;
    public LevelData ActualLevelData { get; set; }

    [Header("Parent")]
    [SerializeField] private Transform StartPointParent;
    [SerializeField] private Transform EndPointParent;
    [SerializeField] private Transform ObstacleParent;
    [SerializeField] private Transform BubbleParent;
    [SerializeField] private Transform BubbleDispoParent;


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

    public void NextLevel() {
        Load(GameData.levelDatas.IndexOf(ActualLevelData) + 1);
    }

    #region LOAD

    public void Load(int levelIndex) {
        Clean();
        if (levelIndex < GameData.levelDatas.Count) {
            ActualLevelData = GameData.levelDatas[levelIndex];
            ActualLevelData.LoadLevelMap(StartPointParent, EndPointParent, ObstacleParent);
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

        levelData.StartPoint = GetEntityFromData(StartPointParent, 0);
        levelData.EndPoints = GetTabEntityFromData(EndPointParent);
        levelData.Obstacles = GetTabEntityFromData(ObstacleParent);

        levelData.Money = MoneyToSave;
        levelData.TailleX = TailleXToSave;
        levelData.TailleY = TailleYToSave;
        levelData.NbrBonhomme = nbrBonhommeToSave;
        levelData.NbrPointEtoile = nbrPointEtoileToSave;

        GameData.levelDatas.Add(levelData);

        AssetDatabase.CreateAsset(levelData, "Assets/ScriptableObjects/LevelData/" + levelName + ".asset");
    }

    private EntityData[] GetTabEntityFromData(Transform parent) {
        var entitys = new EntityData[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
            entitys[i] = GetEntityFromData(parent, i);
        return entitys;
    }

    private EntityData GetEntityFromData(Transform parent, int index) {
        var prefabSource = PrefabUtility.GetCorrespondingObjectFromSource(parent.transform.GetChild(index).gameObject) ;
        return new EntityData() {
            prefab = prefabSource,
            position = parent.transform.GetChild(index).position,
            rotation = parent.transform.GetChild(index).rotation,
            scale = parent.transform.GetChild(index).localScale
        };
    }

    #endregion

    #region CLEAN

    public void Clean() {
        DestroyChilds(StartPointParent);
        DestroyChilds(EndPointParent);
        DestroyChilds(ObstacleParent);
        DestroyChilds(BubbleParent);
        DestroyChilds(BubbleDispoParent);
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
