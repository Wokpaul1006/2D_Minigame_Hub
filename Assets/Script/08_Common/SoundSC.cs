using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSC : Singleton<SoundSC>
{
    [SerializeField] AudioSource theme;

    private int soundMode;
    private int sfxMode;
    public bool allowSFX;
    private void Start()
    {
        allowSFX = false;
        soundMode = PlayerPrefs.GetInt("soundState");
        if (soundMode == 1) PlayTheme();

        sfxMode = PlayerPrefs.GetInt("sfxState");
        if (sfxMode == 1) PlaySFX();


    }

    public void PlayTheme() => theme.Play();
    public void MuteTheme() => theme.Pause();
    public void PlaySFX() => allowSFX = true;
    public void MuteSFX() => allowSFX = false;
}
