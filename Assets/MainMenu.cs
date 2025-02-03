using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        FB.Init();
        GameAnalytics.Initialize();      
    }
    private void Update()
    {
        if (FB.IsInitialized)
        {
            SceneController.LoadNextLevel();
        }
    }

}
