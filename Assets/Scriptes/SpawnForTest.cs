using System.Collections;
using UnityEngine;

public class SpawnForTest : MonoBehaviour {
    public static SpawnForTest Instance;
    public GameObject characterPrefab;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public IEnumerator SpawnCharacter() {
        Instantiate(characterPrefab, transform);
        yield return new WaitForSeconds(2f);
        StartCoroutine(SpawnCharacter());
    }
}