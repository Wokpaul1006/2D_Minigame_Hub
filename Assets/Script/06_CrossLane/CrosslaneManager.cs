using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CrosslaneManager : MonoBehaviour
{
    //No. of Game" #05
    //Rule:
    //Player press the 

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
    [SerializeField] GameObject playerSpawner;

    // Start is called before the first frame update
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
        UpdateGameState(0);//Idle
        coundownNumber = 5;
    }
    void HandleUIs()
    {
        startCoundownTxt.text = coundownNumber.ToString();
    }
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

    public void ToHome() => sceneMN.LoadScene(1, true);
}
