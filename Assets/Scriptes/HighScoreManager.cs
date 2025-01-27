using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{

    private string filePath;
    public HighScoreListData HighScoreList;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "highscores.json");
        Debug.Log("Highscores File Path: " + filePath);
    }

    // Sauvegarder les highscores
    public void SaveHighScores(HighScoreListData highScoreList)
    {
        string json = JsonUtility.ToJson(highScoreList, true); // Convertir en JSON formaté
        File.WriteAllText(filePath, json);
        Debug.Log("Highscores Saved.");
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
            Debug.Log("No Highscores Found. Returning Empty List.");
            return new HighScoreListData();
        }
    }

    
}
