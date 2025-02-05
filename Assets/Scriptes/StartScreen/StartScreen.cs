using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

    [SerializeField] private GameData GameData;

    private void Awake() {
        if(gameObject.name == "GameLvl") {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeScene() {
        SceneManager.LoadScene("MenuLvL");
    }

    public void Settings() {

    }

    public void Exit() {
        Application.Quit();
    }

    public void ChangeMenu() {
        SceneManager.LoadScene("StartScreen");
    }

    public void GoLvl(int value) {
        GameData.levelToLoad = value;
        SceneManager.LoadScene("SampleScene");
    }
}
