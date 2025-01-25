using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTestManager : MonoBehaviour{
    public static GameTestManager Instance;

    public float money;
    public TextMeshProUGUI text;

    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Update(){
        text.text = money + "L";
    }
}
