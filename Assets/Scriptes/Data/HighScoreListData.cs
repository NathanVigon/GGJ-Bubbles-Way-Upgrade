using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HighScoreListData", menuName = "Data/HighScoreListData", order = 2)]
public class HighScoreListData : ScriptableObject
{
    public List<HighScoreData> data;

    public void AddHighScore(HighScoreData highScore)
    {
        bool found = false;
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].indexLevel == highScore.indexLevel)
            {
                found = true;
                if (data[i].score < highScore.score)
                {
                    data[i].score = highScore.score;
                }
            }
        }
        if (!found)
        {
            data.Add(highScore);
        }
    }
}