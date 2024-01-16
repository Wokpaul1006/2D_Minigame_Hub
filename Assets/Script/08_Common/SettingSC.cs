using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSC : MonoBehaviour
{
    [SerializeField] SoundSC soundMN;

    [SerializeField] Toggle soundTG;
    void Start()
    {
        soundMN = GameObject.Find("SoundMN").GetComponent<SoundSC>();   
    }

    public void OnToggleSound()
    {
        print(soundTG.isOn);
        if (!soundTG.isOn)
        {
            soundMN.MuteSound();
        }
        else soundMN.PlaySound();
    }
}
