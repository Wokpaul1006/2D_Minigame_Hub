using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DigItManager : MonoBehaviour
{
    //No. of Game" #06
    //Rule:
    //Player pop the fruit fall from the spawner, if miss more tha 5 fruist, game end.

    #region Common zone
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;

    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;

    //Coundown start part
    [SerializeField] Text startCoundownTxt;
    [SerializeField] GameObject coundonwPanel;
    private int coundownNumber;
    private int gameState; //Show state of the game. 0 is idle, 1 is in-play, 2 is end
    #endregion

    //Specific zone

    void Start()
    {
        SettingStart();
        HandleUIs();

        #region Countdown Start
        if (coundownNumber == 5 && coundownNumber >= 0) StartCoroutine(StartCoundown());
        else if (coundownNumber == 0 || coundownNumber <= 0) StopCoroutine(StartCoundown());
        #endregion

        //pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    
    void SettingStart()
    {
        UpdateGameState(0); //Idle
        coundownNumber = 5;
    }
    void HandleUIs()
    {
        startCoundownTxt.text = coundownNumber.ToString();
    }
    public void ToHome() => sceneMN.LoadScene(1, true);
    private IEnumerator StartCoundown()
    {
        yield return new WaitForSeconds(1);
        coundownNumber--;
        startCoundownTxt.text = coundownNumber.ToString();
        if (coundownNumber <= 0)
        {
            coundonwPanel.SetActive(false);
            UpdateGameState(1);
        }
        StartCoroutine(StartCoundown());
    }
    public void UpdateGameState(int state)
    {
        gameState = state;
        if (gameState == 2)
        {
            pausePnl.ShowPanel(true);
            StopAllCoroutines();
        }
    }
}
