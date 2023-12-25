using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public class PlayerRegisSC : MonoBehaviour
{
    [SerializeField] InputField usernameField;
    [SerializeField] HomeSC homeSC;

    [HideInInspector] string playerName;
    private void Start()
    {
        homeSC = GameObject.Find("HomeManage").GetComponent<HomeSC>();
    }
    public void OnSubmitLocal()
    {
        playerName = usernameField.text;
        PlayerPrefs.SetString("PName", playerName);

        SettingNewPlayer();
    }
    public void OnLogByGG() 
    {
        //Get player name from GG
        PlayerPrefs.SetString("PName", playerName);
        SettingNewPlayer();
    }
    public void OnLogByFB() 
    {
        //Get player name from Facebook
        PlayerPrefs.SetString("PName", playerName);
        SettingNewPlayer();
    }
    private void SettingNewPlayer()
    {
        PlayerPrefs.SetInt("HasPlayed", 1);
        PlayerPrefs.SetInt("PTotalScore", 0);
        PlayerPrefs.SetInt("PHighestScore", 0);
        PlayerPrefs.SetInt("PHighestLevel", 0);
        homeSC.LoadUser();
        gameObject.SetActive(false);
        
    }
}
