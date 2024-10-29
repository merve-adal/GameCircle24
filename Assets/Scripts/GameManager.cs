using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPlayable = true;
    public bool IsPlayable { get => isPlayable; set => isPlayable = value; }

    private int numberOfVehicles = 0;
    private int totalNumberOfVehicles = 0;

    private int lives = 0;
    private int totalLives = 0;

    private int money = 0;
    public int Money { get => money; } 

    public int Lives { get => lives;}
    [SerializeField]
    private LevelLivesScriptableObject levelLives;

    //UILevelInfo uiLevelInfo;
    //UILevelMenu uILevelMenu;
    MenuManager menuManager;

    private void Awake()
    {
        numberOfVehicles = GameObject.FindGameObjectsWithTag("Vehicle").Length;
        lives =levelLives.TotalLivesOfLevel(SceneController.CurrentLevelNumber());
        //uiLevelInfo = GameObject.FindGameObjectWithTag("UILevelInfo").GetComponent<UILevelInfo>();
        //uILevelMenu = GameObject.FindGameObjectWithTag("UILevelMenu").GetComponent<UILevelMenu>();
        menuManager = GameObject.FindGameObjectWithTag("UILevelMenu").GetComponent<MenuManager>();
        //uiLevelInfo.LevelName = "Level " + SceneController.CurrentLevelNumber();
    }
    private void Start()
    {
        //uiLevelInfo.LevelLives = lives.ToString();
        totalLives = lives;
        menuManager.UpdateLives(lives,totalLives);
        totalNumberOfVehicles = numberOfVehicles;
    }
    public void DecreaseNumberOfVehicles()
    {
        numberOfVehicles--;
        menuManager.UpdateLevelSlider(numberOfVehicles, totalNumberOfVehicles);
        if (numberOfVehicles == 0)
        {
            win();
        }
    }
    private void win()
    {
        Debug.Log("level completed");
        SceneController.LevelCompleted();
        //uILevelMenu.OpenWin();
        menuManager.ShowCompletePanel();
        SoundController.PlayWinSound();
    }
    public void DecreaseLives()
    {
        lives--;
        //uiLevelInfo.LevelLives = lives.ToString();    
        menuManager.UpdateLives(lives, totalLives);
        if (lives == 0)
        {
            die();
        }
    }
    private void die()
    {
        Debug.Log("Game over");
        isPlayable = false;
        //uILevelMenu.OpenLose();
        menuManager.ShowLostPanel();
        SoundController.PlayLoseSound();
    }

}
