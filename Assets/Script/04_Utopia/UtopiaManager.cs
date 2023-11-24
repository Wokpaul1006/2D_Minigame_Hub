using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtopiaManager : MonoBehaviour
{
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;

    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;

    private int gameState;
    void Start()
    {
        
    }
    private void SettingStart()
    {
        gameState = 0;
    }

    private void HandleUIs()
    {
        //Everything about update UI must run through this
    }
    private void DecideGameplay()
    {
        //Everything about update gameplay must run through this
    }
    public void ToHome() => sceneMN.LoadScene(1, true);
}
