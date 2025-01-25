using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public static Menu Instance { get; private set; }
    public bool isPaused;
    
    public CanvasGroup gameCanvas;
    public GameObject settingsCanvas;
    
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    
    public void Start() {
        gameCanvas.interactable = true;
        settingsCanvas.SetActive(false);
        isPaused = false;
    }
    
    public void OpenMenu(bool value) {
        gameCanvas.interactable = !value;
        settingsCanvas.SetActive(value);
        Time.timeScale = value ? 0 : 1;
        isPaused = value;
    }
    
    public void OpenSettings() {
        
    }

    public void QuitLevel() {
        SceneManager.LoadScene("Menu");
    }
}