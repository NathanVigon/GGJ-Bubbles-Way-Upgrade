using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{

    public static HighScoreManager Instance;
    private string filePath;
    public HighScoreListData HighScoreList;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "highscores.json");
        print("Highscores File Path: " + filePath);

        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

    }

    // Sauvegarder les highscores
    public void SaveHighScores()
    {
        string json = JsonUtility.ToJson(HighScoreList, true); // Convertir en JSON formaté
        File.WriteAllText(filePath, json);
        print("Highscores Saved.");
    }

    // Charger les highscores
    public HighScoreListData LoadHighScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<HighScoreListData>(json); // Convertir JSON en objet
        }
        else
        {
            print("No Highscores Found. Returning Empty List.");
            return new HighScoreListData();
        }
    }

    
}
