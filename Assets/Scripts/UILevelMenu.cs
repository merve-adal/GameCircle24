using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelMenu : MonoBehaviour
{
    public GameObject Win;
    public GameObject Lose;

    public void OpenWin()
    {
        Win.SetActive(true);
    }
    public void OpenLose()
    {
        Lose.SetActive(true);
    }
    public void ButtonNextLevel()
    {
        SceneController.LoadNextLevel();
    }
    public void ButtonTryAgain()
    {
        SceneController.RestartCurrentLevel();
    }
}
