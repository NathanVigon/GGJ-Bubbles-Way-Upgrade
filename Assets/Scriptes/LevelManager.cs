using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }

    public bool isPlaying = false;

    public float money;
    public TextMeshProUGUI MoneyText;

    [SerializeField] private GameObject PlayButton;
    [SerializeField] private GameObject StopButton;
    public GameObject menuBarDown;
    public GameObject bubbleSelectorPrefab;
    public GameObject[] BulleDispo;

    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private Transform PlayerParent;
    [SerializeField] private List<GameObject> Players;

    private LevelData ActualLevelData;
    private int NbrBonhommeFin;

    void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        
        ChangeMoney(0);
        CollectPrefab(BulleDispo);
    }

    public void SwitchStateGame() {
        StopButton.SetActive(!StopButton.activeSelf);
        PlayButton.SetActive(!PlayButton.activeSelf);
        isPlaying = !isPlaying;
    }

    public void CollectPrefab(GameObject[] prefab) {
        int i = 50;
        foreach(GameObject p in prefab) {
            GameObject selector = Instantiate(bubbleSelectorPrefab, menuBarDown.transform);
            Vector3 position = menuBarDown.transform.position;
            position.x = i;
            i += 100;
            selector.transform.position = position;
            selector.GetComponent<BubbleSelector>().prefab = p;
            selector.GetComponentInChildren<RawImage>().texture = p.GetComponent<Bubble>().sprite;
        }
    }

    public void ChangeMoney(float value){
        money += value;
        MoneyText.text = money + "L";
    }
    public void LoadLevelData() {
        money = ActualLevelData.Money;
        CollectPrefab(ActualLevelData.BulleDispo);
    }

    public void OnClickNextLevel() {
        SceneDataManager.Instance.NextLevel();
        ActualLevelData = SceneDataManager.Instance.ActualLevelData;
    }

    public void OnClickGoMenu() {
        //TODO en attente d'un menu
        throw new NotImplementedException();
    }

    #region PLAY/STOP GAME

    public void StartGame(){
        SwitchStateGame();
        GridManager.Instance.ChangeVisibility(GridManager.Instance.visibility);
        StartCoroutine(SpawnPlayer(1.0f, ActualLevelData.NbrBonhomme));
    }

    public void StopGame() {
        SwitchStateGame();
        GridManager.Instance.ChangeVisibility(GridManager.Instance.visibility);
        for (int i = 0; i < Players.Count; i++) {
            Destroy(Players[i]);
        }
    }

    private IEnumerator SpawnPlayer(float interval, int repeatCount) {
        for (int i = 0; i < repeatCount; i++) {
            //Instantion d'un player 
            Object.Instantiate(PlayerPrefab, ActualLevelData.StartPoint.position + new Vector3(0,1,0), PlayerPrefab.transform.rotation, PlayerParent);
            yield return new WaitForSeconds(interval);
        }
    }

    #endregion
}