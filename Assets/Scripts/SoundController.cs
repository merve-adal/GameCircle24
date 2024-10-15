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
    

    private void Awake()
    {
        instance = this;
    }

    public static void SetVolume(float volume)
    {
        AudioListener.volume= volume;
    }

    public void PlayClickSound()
    {
        instance.clickSound.Play();
    }
    public static void PlayTapSound()
    {
        instance.tapSound.Play();
    }
    public static void PlayCrashSound()
    {
        instance.crashSound.Play();
    }
    public static void PlayWinSound()
    {
        instance.winSound.Play();
    }
    public static void PlayLoseSound()
    {
        instance.loseSound.Play();
    }
    public static void PlayMoneySound()
    {
        instance.moneySound.Play();

    }
    public static void PlayMusicSound()
    {
        instance.musicSound.Play();
    }
}
