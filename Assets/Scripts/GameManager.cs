using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPlayable = true;
    public bool IsPlayable { get => isPlayable; set => isPlayable = value; }

    private int numberOfVehicles = 0;

    SceneController sceneController=new SceneController();

    private int lives = 0;

    public int Lives { get => lives;}
    [SerializeField]
    private LevelLivesScriptableObject levelLives;

    UILevelInfo uiLevelInfo;

    private void Awake()
    {
        numberOfVehicles = GameObject.FindGameObjectsWithTag("Vehicle").Length;
        lives =levelLives.TotalLivesOfLevel(sceneController.CurrentLevelNumber());
        uiLevelInfo= GameObject.FindGameObjectWithTag("UILevelInfo").GetComponent<UILevelInfo>();
    }
    private void Start()
    {
        uiLevelInfo.LevelName = "Level " + sceneController.CurrentLevelNumber();
        uiLevelInfo.LevelLives = lives.ToString();
        
    }
    public void DecreaseNumberOfVehicles()
    {
        numberOfVehicles--;
        if (numberOfVehicles == 0)
        {       
            //int a = SaveLoadGameInfo.LoadLastLevel();
        }
    }
    private void win()
    {
        Debug.Log("level completed");
        sceneController.LevelCompleted();
    }
    public void DecreaseLives()
    {
        lives--;
        uiLevelInfo.LevelLives = lives.ToString();
        if(lives == 0)
        {
            die();
        }
    }
    private void die()
    {
        Debug.Log("Game over");
        isPlayable = false;
        //sceneController.LevelCompleted();
    }
}
