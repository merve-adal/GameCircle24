using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void LoadMainScreen()
    {
        SceneManager.LoadScene(0);
    }
    public static void LoadNextLevel()
    {
        SceneManager.LoadScene("level_"+(SaveLoadGameInfo.LoadLastLevel()+1));
    }
    public static void RestartCurrentLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("level_"+currentScene);
    }
    public static void LevelCompleted()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == SceneManager.sceneCountInBuildSettings - 1)
        {
            SaveLoadGameInfo.SaveLastLevel(4); //skip tutorial, first 3 scenes are tutorials
        }
        else
        {
            SaveLoadGameInfo.SaveLastLevel(currentScene+1);
        }
    }
    public static int CurrentLevelNumber() //numbers in the name 
    {
        string levelName = SceneManager.GetActiveScene().name;
        int levelNumber = int.Parse(levelName.Substring(6)); //level_115
        return levelNumber;
    }
}
