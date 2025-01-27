using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public float DifficultyLevel;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            LevelManager.Instance.AddToScore(DifficultyLevel);
            Destroy(other.gameObject);
        }
    }
}
