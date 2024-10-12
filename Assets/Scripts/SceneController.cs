using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
    public void LoadMainScreen()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLastLevel()
    {
        SceneManager.LoadScene(SaveLoadGameInfo.LoadLastLevel());
    }
    public void LevelCompleted()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == SceneManager.sceneCountInBuildSettings - 1)
        {
            SaveLoadGameInfo.SaveLastLevel(1);
        }
        else
        {
            SaveLoadGameInfo.SaveLastLevel(currentScene+1);//biterse basa don ya da 4.ye don
        }
        //SceneManager.LoadScene(0);
    }
    public int CurrentLevelNumber() //numbers in the name 
    {
        string levelName = SceneManager.GetActiveScene().name;
        int levelNumber = int.Parse(levelName.Substring(6)); //level_115
        return levelNumber;
    }
}
