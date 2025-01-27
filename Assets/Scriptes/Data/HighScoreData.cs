using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreData : ScriptableObject
{
    public int indexLevel;
    public int score;
}

public class HighScoreListData : ScriptableObject
{
    public List<HighScoreData> data;

    public void AddHighScore(HighScoreData highScore)
    {
        bool found=false;
        for (int i = 0; i< data.Count; i++)
        {
            if (data[i].indexLevel == highScore.indexLevel)
            {
                found= true;
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