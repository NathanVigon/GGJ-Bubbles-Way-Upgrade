using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighScoreData", menuName = "Data/HighScoreData", order = 3)]
public class HighScoreData : ScriptableObject
{
    public int indexLevel;
    public double score;

    public HighScoreData(int indexLevel, double score)
    {
        this.indexLevel = indexLevel;
        this.score = score;
    }
}

