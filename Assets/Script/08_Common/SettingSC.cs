using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSC : MonoBehaviour
{
    [SerializeField] SoundSC soundMN;

    [SerializeField] Toggle soundTG;
    [SerializeField] Toggle sfxToggle;
    void Start()
    {
        soundMN = GameObject.Find("SoundMN").GetComponent<SoundSC>();   
    }

    public void OnToggleSound()
    {
        if (!soundTG.isOn)
        {
            PlayerPrefs.SetInt("soundState", 0);
            soundMN.MuteTheme();
        }
        else
        {
            PlayerPrefs.SetInt("soundState", 1);
            soundMN.PlayTheme();
        } 
    }
    public void OnToggleSFX()
    {
        if (!sfxToggle.isOn)
        {
            PlayerPrefs.SetInt("sfxState", 0);
            soundMN.MuteSFX();
        }
        else
        {
            PlayerPrefs.SetInt("sfxState", 1);
            soundMN.PlaySFX();
        } 
    }
    public void ExitGame() => Application.Quit();
}
