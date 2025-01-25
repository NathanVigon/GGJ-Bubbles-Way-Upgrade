using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }

    public GameObject playButton;
    public GameObject stopButton;
    public bool isPlaying;

    public float money;
    public TextMeshProUGUI text;

    public GameObject menuBarDown;
    public GameObject bubbleSelectorPrefab;
    public GameObject[] prefabsForTest;

    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        
        playButton.SetActive(true);
        stopButton.SetActive(false);
        ChangeMoney(0);
        CollectPrefab(prefabsForTest);
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
        text.text = money + "L";
    }
    
    public void StartGame(bool value){
        isPlaying = value;
        playButton.SetActive(!value);
        stopButton.SetActive(value);
        GridManager.Instance.ChangeVisibility(GridManager.Instance.visibility);
        Movement[] movements = FindObjectsOfType<Movement>();
        foreach (Movement movement in movements) {
            movement.BougeFilsDe();
        }
        
    }
}