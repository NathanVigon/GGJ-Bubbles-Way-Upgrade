using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }

    public bool isPlaying;
    public bool isWin;
    public bool canPlay;

    public int money;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject CanvasWin;

    [SerializeField] private GameObject PlayButton;
    [SerializeField] private GameObject StopButton;
    public GameObject menuBarDown;
    public GameObject bubbleSelectorPrefab;
    [SerializeField] private GameObject[] StarsJaune;
    [SerializeField] private GameObject[] StarsGris;

    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private Transform PlayerParent;
    private List<GameObject> Players = new();

    private int indexLevel;
    public GameObject canvasTutorials;
    private Queue<string> tutorialQueue = new Queue<string>();
    
    private double score;
    
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
        indexLevel = 0;
        SceneDataManager.Instance.Load(indexLevel);
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
        canPlay = false;
        isWin = false;
        ShowTutorial();
    }
    
    private void ShowTutorial() {
        tutorialQueue.Clear();
        switch (indexLevel) {
            case 0:
                tutorialQueue.Enqueue("PoseBubbles");
                tutorialQueue.Enqueue("EraseBubbles");
                tutorialQueue.Enqueue("BlueBubbles");
                tutorialQueue.Enqueue("Goal");
                break;
            case 1:
                tutorialQueue.Enqueue("GreenBubbles");
                tutorialQueue.Enqueue("RedBubbles");
                tutorialQueue.Enqueue("Spikes");
                break;
            case 2:
                tutorialQueue.Enqueue("PurpleBubbles");
                break;
            case 3:
                tutorialQueue.Enqueue("YellowBubbles");
                break;
            case 4:
                tutorialQueue.Enqueue("BlackBubbles");
                break;
        }
        ShowNextTutorial();
    }
    
    private void ShowNextTutorial() {
        if (tutorialQueue.Count > 0) {
            string nextTutorial = tutorialQueue.Dequeue();
            Transform tutorialTransform = canvasTutorials.transform.Find(nextTutorial);
            if (tutorialTransform != null) {
                tutorialTransform.gameObject.SetActive(true);
                tutorialTransform.GetComponent<Button>().onClick.AddListener(OnTutorialClicked);
            }
        } else {
            canPlay = true;
        }
    }

    private void OnTutorialClicked() {
        foreach (Transform child in canvasTutorials.transform) {
            child.gameObject.SetActive(false);
        }
        ShowNextTutorial();
    }

    #region WIN CANVAS

    public void OnClickNextLevel() {
        ResetStars();
        CanvasWin.SetActive(false);
        SwitchStateGame();
        SceneDataManager.Instance.NextLevel();
        indexLevel++;
        ActualLevelData = SceneDataManager.Instance.ActualLevelData;
        LoadLevelData();
    }

    public void OnClickRetry() {
        ResetStars();
        CanvasWin.SetActive(false);
        SwitchStateGame();
        isWin = false;
        canPlay = true;
    }

    public void OnClickGoMenu() {
        ResetStars();
        CanvasWin.SetActive(false);
        //TODO en attente d'un menu
        throw new NotImplementedException();
    }
    
    private int TrueCountPlayers()
    {
        int count = 0;
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i] != null)
            {
                count++;
            }
        }
        return count;
    }
    private IEnumerator LevelFinished()
    {
        while (TrueCountPlayers() > 0)
        { 
            yield return new WaitForSeconds(TrueCountPlayers()-0.9f);
        }
        if (score >= ActualLevelData.NbrPointEtoile[0])
        {
            LevelWin();
        }
        else
        {
            SwitchStateGame();
        }
    }

    private void LevelWin() {
        isWin = true;
        CanvasWin.SetActive(true);
        ScoreText.text = "Score : " + score;
        for(int i = 0; i < ActualLevelData.NbrPointEtoile.Length; i++) {
            if (score >= ActualLevelData.NbrPointEtoile[i]) {
                StarsJaune[i].SetActive(true);
            } else {
                StarsGris[i].SetActive(true);
            }
        }
    }

    public void AddToScore(float difficultyLevelEndPoint) {
        float moneyLeft = money;
        float moneyTot = ActualLevelData.Money;
        float propMoney =  (float)(moneyLeft / moneyTot);
        score += Math.Round( 100 * difficultyLevelEndPoint * (1.0f+propMoney));
        print(score);
    }

    private void ResetStar(GameObject[] stars) {
        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive(false);
        }
    }

    private void ResetStars() {
        ResetStar(StarsJaune);
        ResetStar(StarsGris);
    }

    #endregion

    #region PLAY/STOP GAME

    public void StartGame(){
        if (isWin || !canPlay) return;
        
        SwitchStateGame();
        score = 0;
        GridManager.Instance.ChangeVisibility(GridManager.Instance.visibility);
        StartCoroutine(SpawnPlayer(1.0f, ActualLevelData.NbrBonhomme));
        StartCoroutine(LevelFinished());
    }

    public void StopGame() {
        if (isWin) return;
        
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