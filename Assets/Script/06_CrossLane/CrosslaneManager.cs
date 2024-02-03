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
    [SerializeField] List<GameObject> spawnListLeft = new List<GameObject>();
    [SerializeField] List<GameObject> spawnListRight = new List<GameObject>();
    [SerializeField] GameObject spawnLeft, spawnRight;
    [SerializeField] GameObject playerSpawn, target;

    internal int curLvl; //Use this variable for entire gameplay
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

        //pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    void SettingStart()
    {
        UpdateGameState(0);//Idle
        coundownNumber = 5;
        curLvl = baseLvl;
        CheckLevel(curLvl);
        SpawnPlayer(playerSpawn.transform.position);
    }
    #region UIs side Handle Zone
    public void ToHome() => sceneMN.LoadScene(1, true);
    void HandleUIs()
    {
        if(curLvl == 1) CoundownToStartTxt.text = coundownNumber.ToString();
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
    } //This function is in use of countdown 5 to 0 at the start of the game.
    private void SpawnPlayer(Vector3 pos) 
    {
        //Call this every time refresh of level
         player = Instantiate(player, pos, Quaternion.identity);
    }
    public void LevelUp()
    {
        curLvl++;
        HandleUIs();
        SpawnPlayer(playerSpawn.transform.position);
        CheckLevel(curLvl);
    }

    private void CheckLevel(int lvl)
    {
        if (lvl > 0 && lvl <= 100)
        {
            //Generate of 4 lane
            if (spawnListLeft != null) spawnListLeft.Clear();
            float startPosY = 3;
            target.transform.position = new Vector3(0, startPosY + 1, 0);
            playerSpawn.transform.position = new Vector3(0, -(startPosY + 1), 0);
            for (int i = 0; i < 7; i++)
            {
                if (i == 0)
                {
                    spawnListLeft.Add(Instantiate(spawnLeft, new Vector3(-3, startPosY, 0), Quaternion.identity));
                    spawnListRight.Add(Instantiate(spawnRight, new Vector3(3, startPosY, 0), Quaternion.identity));
                }
                else
                {
                    spawnListLeft.Add(Instantiate(spawnLeft, new Vector3(-3, startPosY, 0), Quaternion.identity));
                    spawnListRight.Add(Instantiate(spawnRight, new Vector3(3, startPosY, 0), Quaternion.identity));
                }
                startPosY -= 1;
            }

            if (lvl <= 10)
            {
                //Active spawner Spawner Left 1, 3
                //Active spawner Spawner Right 2, 4
                //Delay spawn time random between 5 - 10s
            }
            else if (lvl > 10 && lvl <= 20)
            {
                //Active spawner Spawner Left 2, 4
                //Active spawner Spawner Right 1, 3
                //Delay spawn time random between 2.5 - 7.5s
            }
            else if (lvl > 20 && lvl <= 40)
            {
                //Active spawner Spawner Left 1, 4
                //Active spawner Spawner Right 2, 3
                //Delay spawn time random between 2s - 5s
            }
            else if (lvl > 40 && lvl <= 80)
            {
                //Active spawner Spawner Left 1, 2,3, 4
                //Active spawner Spawner Right 1,2,3,4
                //Delay spawn time random between 5s - 10s
            }
            else if (lvl > 80 && lvl <= 100)
            {
                //Random Active spawner Spawner Left 1, 2,3, 4
                //Random Active spawner Spawner Right 1,2,3,4
                //Delay spawn time random between 2.5 - 7
            }
        }
        else if(lvl > 100 && lvl <= 200)
        {
            //Generate of 8 lane
            if (spawnListLeft != null) spawnListLeft.Clear();
            float startPosY = 5;
            target.transform.position = new Vector3(0, startPosY + 1, 0);
            playerSpawn.transform.position = new Vector3(0, -(startPosY - 1), 0);
            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                {
                    spawnListLeft.Add(Instantiate(spawnLeft, new Vector3(-3, startPosY, 0), Quaternion.identity));
                    spawnListRight.Add(Instantiate(spawnRight, new Vector3(3, startPosY, 0), Quaternion.identity));
                }
                else
                {
                    spawnListLeft.Add(Instantiate(spawnLeft, new Vector3(-3, startPosY, 0), Quaternion.identity));
                    spawnListRight.Add(Instantiate(spawnRight, new Vector3(3, startPosY, 0), Quaternion.identity));
                }
                startPosY -= 1;
            }

        }
        else if(lvl > 200 && lvl <= 300)
        {
            //Generate of 12 lane
            if (spawnListLeft != null) spawnListLeft.Clear();
            float startPosY = 7;
            target.transform.position = new Vector3(0, startPosY + 1, 0);
            playerSpawn.transform.position = new Vector3(0, -(startPosY - 1), 0);
            for (int i = 0; i < 15; i++)
            {
                if (i == 0)
                {
                    spawnListLeft.Add(Instantiate(spawnLeft, new Vector3(-3, startPosY, 0), Quaternion.identity));
                    spawnListRight.Add(Instantiate(spawnRight, new Vector3(3, startPosY, 0), Quaternion.identity));
                }
                else
                {
                    spawnListLeft.Add(Instantiate(spawnLeft, new Vector3(-3, startPosY, 0), Quaternion.identity));
                    spawnListRight.Add(Instantiate(spawnRight, new Vector3(3, startPosY, 0), Quaternion.identity));
                }
                startPosY -= 1;
            }
        }
        else if(lvl > 300 && lvl <= 400)
        {
            //Generate of 16 lane
            if (spawnListLeft != null) spawnListLeft.Clear();
            float startPosY = 9;
            target.transform.position = new Vector3(0, startPosY + 1, 0);
            playerSpawn.transform.position = new Vector3(0, -(startPosY - 1), 0);
            for (int i = 0; i < 19; i++)
            {
                if (i == 0)
                {
                    spawnListLeft.Add(Instantiate(spawnLeft, new Vector3(-3, startPosY, 0), Quaternion.identity));
                    spawnListRight.Add(Instantiate(spawnRight, new Vector3(3, startPosY, 0), Quaternion.identity));
                }
                else
                {
                    spawnListLeft.Add(Instantiate(spawnLeft, new Vector3(-3, startPosY, 0), Quaternion.identity));
                    spawnListRight.Add(Instantiate(spawnRight, new Vector3(3, startPosY, 0), Quaternion.identity));
                }
                startPosY -= 1;
            }
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
