using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;

    [SerializeField] private AudioSource clickSound;
    [SerializeField] private AudioSource tapSound;
    [SerializeField] private AudioSource crashSound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource loseSound;
    [SerializeField] private AudioSource moneySound;
    [SerializeField] private AudioSource musicSound;
    
    private bool isVolumeOn=false;
    private bool isVibrationOn = false;

    private void Awake()
    {
        instance = this;
        CheckVolumeOnOff();
        CheckVibrationOnOff();
        AudioListener.volume = 1f;
        
    }

    public static void SetVolume(float volume)
    {
        AudioListener.volume= volume;
    }
    public static void SetVibration(bool statue)
    {
        instance.isVibrationOn= statue;
    }

    public static void PlayClickSound()
    {
        //playSound(instance.clickSound);
        instance.clickSound.Play();
        vibrate();
    }
    public static void PlayTapSound()
    {
        //playSound(instance.tapSound);
        instance.tapSound.Play();
        vibrate();
    }
    public static void PlayCrashSound()
    {
        //playSound(instance.crashSound);
        instance.crashSound.Play();
        vibrate();
    }
    public static void PlayWinSound()
    {
        //playSound(instance.winSound);
        instance.winSound.Play();
    }
    public static void PlayLoseSound()
    {
        //playSound(instance.loseSound);
        instance.loseSound.Play();
    }
    public static void PlayMoneySound()
    {
        //playSound(instance.moneySound);
        instance.moneySound.Play();
    }
    public static void PlayMusicSound()  //play on awake
    {
        //instance.musicSound.Play();  
    }

    //private void playSound(AudioSource audioSource)
    //{
    //    if (instance.isVolumeOn)
    //    {
    //        audioSource.Play();
    //    }
    //}
    public static void CheckVolumeOnOff()
    {
        if (SaveLoadGameInfo.LoadAuidoOnOff())
        {
            instance.isVolumeOn = true;
            SetVolume(1f);
        }
        else
        {
            instance.isVolumeOn = false;
            SetVolume(0f);
        }
    }
    public static void CheckVibrationOnOff()
    {
        if (SaveLoadGameInfo.LoadVibrationOnOff())
        {
            instance.isVibrationOn = true;
        }
        else
        {
            instance.isVibrationOn = false;
        }
    }
    private static void vibrate()
    {
        if (instance.isVibrationOn)
        {
            Handheld.Vibrate();
        }
    }
}
