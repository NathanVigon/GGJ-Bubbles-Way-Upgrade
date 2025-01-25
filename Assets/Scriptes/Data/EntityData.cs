using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityData {
    public GameObject prefab;
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;

    public void InstantiateEntity(Transform parent) {
        GameObject element = Object.Instantiate(prefab, position, rotation, parent);
        element.transform.localScale = scale;
    }
}
