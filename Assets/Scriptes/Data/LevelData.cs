using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
public class LevelData : ScriptableObject {
    public EntityData StartPoint;
    public EntityData EndPoint;
    public EntityData[] Obstacles;

    public void LoadLevel(Transform StartPointParent, Transform EndPointParent, Transform ObstacleParent) {
        StartPoint.InstantiateEntity(StartPointParent);
        EndPoint.InstantiateEntity(EndPointParent);
        for (int i = 0; i < Obstacles.Length; i++)
            Obstacles[i].InstantiateEntity(ObstacleParent);
    }
}
