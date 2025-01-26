using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public static Menu Instance { get; private set; }
    public bool isPaused;
    
    public CanvasGroup gameCanvas;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    
    public AudioSource musicSource;
    public Slider volumeSlider;
    
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    
    public void Start() {
        gameCanvas.interactable = true;
        menuPanel.SetActive(false);
        isPaused = false;
    }

    public void OpenMenu(bool value) {
        gameCanvas.interactable = !value;
        menuPanel.SetActive(value);
        settingsPanel.SetActive(false);
        Time.timeScale = value ? 0 : 1;
        isPaused = value;
    }
    
    public void OpenSettings() {
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    
    public void CloseSettings() {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    
    public void SetVolume(float volume) {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void QuitLevel() {
        SceneManager.LoadScene("Menu");
    }
}