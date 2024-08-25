using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButtonToggle : MonoBehaviour
{

    private bool isAudioOn = false;

    void Start()
    {
        if (SaveLoadGameInfo.LoadAuidoOnOff())
            isAudioOn = true;
        else
            isAudioOn = false;

        if (isAudioOn == false)
        {
            AudioListener.volume = 0f;
        }
        else if (isAudioOn == true)
        {
            AudioListener.volume = 1.0f;
        }
    }

    public void ToggleSound()
    {
        if (isAudioOn == false)
        {
            TurnOnSound();
        }
        else if (isAudioOn == true)
        {
            TurnOffSound();
        }
    }

    private void TurnOnSound()
    {
        isAudioOn = true;
        AudioListener.volume = 1.0f;
        SaveLoadGameInfo.SaveAudioOnOff(isAudioOn);
    }
    private void TurnOffSound()
    {
        isAudioOn = false;
        AudioListener.volume = 0f;
        SaveLoadGameInfo.SaveAudioOnOff(isAudioOn);
    }
}
