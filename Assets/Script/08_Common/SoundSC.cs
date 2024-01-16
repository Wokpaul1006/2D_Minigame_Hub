using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSC : Singleton<SoundSC>
{
    [SerializeField] AudioSource theme;

    private int soundMode;
    private void Start()
    {
        soundMode = PlayerPrefs.GetInt("soundState");
        if (soundMode == 1) PlaySound();
    }

    public void PlaySound() => theme.Play();
    public void MuteSound() => theme.Pause();
}
