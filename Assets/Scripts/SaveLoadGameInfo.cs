using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGameInfo
{
    public static int LoadLastLevel()
    {
        return PlayerPrefs.GetInt("Level",1);
    }
    public static void SaveLastLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
    }
    public static bool LoadAuidoOnOff()
    {
        int audioState = PlayerPrefs.GetInt("Audio", 0);
        if(audioState == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void SaveAudioOnOff(bool state)
    {
        if (state) {
            PlayerPrefs.SetInt("Audio", 1);         
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 0);
        }
        PlayerPrefs.Save();
    }
    public static bool LoadVibrationOnOff()
    {
        int vibrationState = PlayerPrefs.GetInt("Vibration", 0);
        if (vibrationState == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void SaveVibrationOnOff(bool state)
    {
        if (state)
        {
            PlayerPrefs.SetInt("Vibration", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Vibration", 0);
        }
        PlayerPrefs.Save();
    }
}
