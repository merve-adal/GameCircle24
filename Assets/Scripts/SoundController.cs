using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController soundController;

    [SerializeField] private AudioSource clickSound;
    [SerializeField] private AudioSource tapSound;
    [SerializeField] private AudioSource crashSound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource musicSound;

    private void Awake()
    {
        soundController = this;
    }
    public void playClickSound()
    {
        clickSound.Play();
    }
    public void playTapSound()
    {
        tapSound.Play();
    }
    public void playCrashSound()
    {
        crashSound.Play();
    }
    public void playWinSound()
    {
        winSound.Play();
    }
    public void playMusincSound()
    {
        musicSound.Play();
    }
}
