using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }

    public bool isPlaying = false;
    public bool isWin = false;

    public int money;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject CanvasWin;

    [SerializeField] private GameObject PlayButton;
    [SerializeField] private GameObject StopButton;
    public GameObject menuBarDown;
    public GameObject bubbleSelectorPrefab;

    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private Transform PlayerParent;
    private List<GameObject> Players = new();

    private LevelData ActualLevelData {
        get {return SceneDataManager.Instance.ActualLevelData;}
        set {SceneDataManager.Instance.ActualLevelData = value;}
    }

    void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        SceneDataManager.Instance.Load(0);
        LoadLevelData();
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

    public void ChangeMoney(int value){
        money += value;
        MoneyText.text = money + "L";
    }
    public void LoadLevelData() {
        money = ActualLevelData.Money;
        CollectPrefab(ActualLevelData.BulleDispo);
        ChangeMoney(0);
        Cursor.Instance.SetPrefab(null);
    }

    #region WIN CANVAS

    public void OnClickNextLevel() {
        CanvasWin.SetActive(false);
        SwitchStateGame();
        SceneDataManager.Instance.NextLevel();
        ActualLevelData = SceneDataManager.Instance.ActualLevelData;
        LoadLevelData();
    }

    public void OnClickGoMenu() {
        CanvasWin.SetActive(false);
        //TODO en attente d'un menu
        throw new NotImplementedException();
    }

    public void LevelWin(int difficultyLevelEndPoint) {
        CanvasWin.SetActive(true);
        int score = CalculScore(difficultyLevelEndPoint);
        ScoreText.text = "Score : " + score;
    }

    private int CalculScore(int difficultyLevelEndPoint) {
        return 0;
    }

    #endregion

    #region PLAY/STOP GAME

    public void StartGame(){
        SwitchStateGame();
        GridManager.Instance.ChangeVisibility(GridManager.Instance.visibility);
        StartCoroutine(SpawnPlayer(1.0f, ActualLevelData.NbrBonhomme));
    }

    public void StopGame() {
        SwitchStateGame();
        GridManager.Instance.ChangeVisibility(GridManager.Instance.visibility);
        StopAllCoroutines();
        KillAllPlayer();
    }

    private IEnumerator SpawnPlayer(float interval, int repeatCount) {
        for (int i = 0; i < repeatCount; i++) {
            //Instantion d'un player 
            Players.Add(Instantiate(PlayerPrefab, ActualLevelData.StartPoint.position + new Vector3(-0.5f,1,0), PlayerPrefab.transform.rotation, PlayerParent));
            yield return new WaitForSeconds(interval);
        }
    }

    private void KillAllPlayer() {
        for (int i = 0; i < Players.Count; i++) {
            Destroy(Players[i]);
        }
    }

    #endregion
}