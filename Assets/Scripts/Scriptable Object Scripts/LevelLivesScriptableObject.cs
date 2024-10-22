using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelLives", menuName = "ScriptableObjects/LevelLivesScriptableObject")]
public class LevelLivesScriptableObject : ScriptableObject
{
    [SerializeField]
    private int[] levelsLives;

    public int TotalLivesOfLevel(int level)
    {
        return levelsLives[level-1];
    }
    
}
