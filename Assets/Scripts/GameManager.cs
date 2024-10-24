using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPlayable = true;
    public bool IsPlayable { get => isPlayable; set => isPlayable = value; }

    private int numberOfVehicles = 0;

    private int lives = 0;

    private int money = 0;
    public int Money { get => money; } 

    public int Lives { get => lives;}
    [SerializeField]
    private LevelLivesScriptableObject levelLives;

    UILevelInfo uiLevelInfo;
    UILevelMenu uILevelMenu;

    private void Awake()
    {
        numberOfVehicles = GameObject.FindGameObjectsWithTag("Vehicle").Length;
        lives =levelLives.TotalLivesOfLevel(SceneController.CurrentLevelNumber());
        uiLevelInfo = GameObject.FindGameObjectWithTag("UILevelInfo").GetComponent<UILevelInfo>();
        uILevelMenu = GameObject.FindGameObjectWithTag("UILevelMenu").GetComponent<UILevelMenu>();
        uiLevelInfo.LevelName = "Level " + SceneController.CurrentLevelNumber();
    }
    private void Start()
    {     
        uiLevelInfo.LevelLives = lives.ToString();
    }
    public void DecreaseNumberOfVehicles()
    {
        numberOfVehicles--;
        if (numberOfVehicles == 0)
        {
            win();
        }
    }
    private void win()
    {
        Debug.Log("level completed");
        SceneController.LevelCompleted();
        uILevelMenu.OpenWin();
        SoundController.PlayWinSound();
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
        uILevelMenu.OpenLose();
        SoundController.PlayLoseSound();
    }

}
