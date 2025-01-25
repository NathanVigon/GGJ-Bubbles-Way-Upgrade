using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTestManager : MonoBehaviour{
    public static GameTestManager Instance { get; private set; }

    public GameObject playButton;
    public GameObject stopButton;
    public bool isPlaying;

    public float money;
    public TextMeshProUGUI text;

    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        
        playButton.SetActive(true);
        stopButton.SetActive(false);
        ChangeMoney(0);
    }

    public void ChangeMoney(float value){
        money += value;
        text.text = money + "L";
    }
    
    public void StartGame(bool value){
        isPlaying = value;
        playButton.SetActive(!value);
        stopButton.SetActive(value);
        GridManager.Instance.ChangeVisibility(GridManager.Instance.visibility);
    }
}
