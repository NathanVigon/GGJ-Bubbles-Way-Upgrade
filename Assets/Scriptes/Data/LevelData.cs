using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
public class LevelData : ScriptableObject {
    public EntityData StartPoint;
    public EntityData[] EndPoints;
    public EntityData[] Obstacles;

    public GameObject[] BulleDispo;

    public int Money;
    public int TailleX;
    public int TailleY;
    public int NbrBonhomme;
    public int[] NbrPointEtoile;


    public void LoadLevelMap(Transform StartPointParent, Transform EndPointParent, Transform ObstacleParent) {
        StartPoint.InstantiateEntity(StartPointParent);
        LoadTabEntity(EndPointParent, EndPoints);
        LoadTabEntity(ObstacleParent, Obstacles);
    }

    public void LoadTabEntity(Transform parent, EntityData[] tabEntity) {
        for (int i = 0; i < tabEntity.Length; i++)
            tabEntity[i].InstantiateEntity(parent);
    }
}
