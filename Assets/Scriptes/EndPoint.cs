using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public int DifficultyLevel;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            LevelManager.Instance.LevelWin(DifficultyLevel);
            Destroy(other.gameObject);
        }
    }
}
