using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CrosslaneManager : MonoBehaviour
{
    //No. of Game" #05
    //Rule:
    //Player press the "GO" button to step forward. Press Navigator button to re-navigating player
    //Try to reach target at the top to get new level
    //If player hit car, game false
    //Car will increase their speed for 0.1f per level
    //Car Spawner will spawn fasster and more densier than every level
    //Score count by how many car spawned and destrotroyed by each level

    #region Common zone
    [SerializeField] Text curretScoreText;
    [SerializeField] Text currentLevelText;

    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;

    private int gameState; //Show state of the game. 0 is idle, 1 is in-play, 2 is end

    //Coundown start part
    [SerializeField] Text CoundownToStartTxt;
    [SerializeField] GameObject coundonwPanel;
    [SerializeField] CrosslaneCharSC player;
    private int coundownNumber;
    #endregion

    //Specific zone
    [HideInInspector] Vector3 playerSpawnPos;
    [SerializeField] List<CrossLaneSpawner> spawnList = new List<CrossLaneSpawner>();

    private int curLvl; //Use this variable for entire gameplay
    private int curScore; //Use this variable for entire gameplay
    private int baseLvl = 1;

    // Start is called before the first frame update
    void Start()
    {
        SettingStart();
        HandleUIs();

        #region Countdown Start
        if (coundownNumber == 5 && coundownNumber >= 0) StartCoroutine(CoundownToStart());
        else if (coundownNumber == 0 || coundownNumber <= 0) StopCoroutine(CoundownToStart());
        #endregion

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    void SettingStart()
    {
        UpdateGameState(0);//Idle
        coundownNumber = 5;
        curLvl = baseLvl;
        playerSpawnPos = new Vector3(0, -3, 0);
        SpawnPlayer(playerSpawnPos);
    }
    #region UIs side Handle Zone
    public void ToHome() => sceneMN.LoadScene(1, true);
    void HandleUIs()
    {
        if(curLvl == 1)
        {
            CoundownToStartTxt.text = coundownNumber.ToString();
        }
        UpdateCurrentScoreText(curScore);
        UpdateCurrentLevelText(curLvl);
    }
    private void UpdateCurrentScoreText(int score) => curretScoreText.text = score.ToString();
    private void UpdateCurrentLevelText(int lvl) => currentLevelText.text = lvl.ToString();
    #endregion

    #region Handle Gameplay Logic Zone
    public void UpdateGameState(int state)
    {
        gameState = state;
        if (gameState == 2)
        {
            pausePnl.ShowPanel(true);
            StopAllCoroutines();
        }
    }
    private IEnumerator CoundownToStart()
    {
        yield return new WaitForSeconds(1);
        coundownNumber--;
        CoundownToStartTxt.text = coundownNumber.ToString();
        if (coundownNumber <= 0)
        {
            coundonwPanel.SetActive(false);
            UpdateGameState(1);
        }
        StartCoroutine(CoundownToStart());
    }
    private void SpawnPlayer(Vector3 pos) 
    {
        //Call this every time refresh of level
         player = Instantiate(player, pos, Quaternion.identity);
    }
    public void LevelUp()
    {
        curLvl++;
        HandleUIs();
        SpawnPlayer(playerSpawnPos);
        for(int i = 0; i<= spawnList.Count; i++) 
        {
            spawnList[i].UpdateDelayTime(curLvl);
        }
    }
    #endregion

    #region Controlling Side
    public void MoveLeft() => player.SetDir(1);
    public void MoveRight() => player.SetDir(2);
    public void MoveUp() => player.SetDir(3);
    #endregion
    public void ShowPause()
    {
        pausePnl.ShowPanel(true);
        StopAllCoroutines();
    }
}
