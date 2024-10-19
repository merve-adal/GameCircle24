using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;

    [SerializeField] private AudioSource clickSound;
    [SerializeField] private AudioSource tapSound;
    [SerializeField] private AudioSource crashSound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource musicSound;

    private void Awake()
    {
        instance = this;
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume= volume;
    }
    public void PlayClickSound()
    {
        clickSound.Play();
    }
    public void PlayTapSound()
    {
        tapSound.Play();
    }
    public void PlayCrashSound()
    {
        crashSound.Play();
    }
    public void PlayWinSound()
    {
        winSound.Play();
    }
    public void PlayMusicSound()
    {
        musicSound.Play();
    }
}
