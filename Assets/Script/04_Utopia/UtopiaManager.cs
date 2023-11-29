using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class UtopiaManager : MonoBehaviour
{
    //No. of Game" #04
    //Rule:
    //Player jump to reach new footstep, if fall 3 time, game end.
    //Every time player hit new footstep, plus one player score.

    //Common zone
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;
    [SerializeField] Text startCoundownTxt;
    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;
    [SerializeField] GameObject coundonwPanel;
    private int coundownNumber;

    //Specific zone
    [SerializeField] List<GameObject> stepList = new List<GameObject>();
    [SerializeField] UtopiaCharSC character;
    [SerializeField] GameObject theTide;
    [HideInInspector] Vector3 startPos = new Vector3(-2,0, 0);
    [HideInInspector] Vector3 nextStepPos;

    private int gameState; //Show state of the game. 0 is idle, 1 is in-play, 2 is end
    private int baseScore = 0;
    private int baseLevel = 1;
    private int gameplayDir; //Direction of both stepfoot and player. 0 = head 2, 1 = head -2
    private int playerDir;
    private int randStepOder; //Random oder of footstep in the list
    private float randStepX, randStepY;
    void Start()
    {
        SettingStart();
        HandleUIs();

        #region Countdown Start
        if (coundownNumber == 5 && coundownNumber >= 0) StartCoroutine(StartCoundown());
        else if(coundownNumber == 0 || coundownNumber <= 0) StopCoroutine(StartCoundown());
        #endregion

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    private void SettingStart()
    {
        UpdateGameState(0);//Idle
        baseScore = 0;
        baseLevel = 0;
        coundownNumber = 5;
        randStepY = -3;
        randStepX = 0;

        gameplayDir = 0;
        playerDir = 0;
    }

    #region Internal Handle
    private void HandleUIs()
    {
        curretScore.text = baseScore.ToString();
        currentLevel.text = baseLevel.ToString();
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
            character.DecidePlayeState(1);
            DecideStepSpawn();
            StartCoroutine(RisingTide());
        }
        StartCoroutine(StartCoundown());
    }
    #endregion

    #region Gameplay Handle
    private void DecideStepSpawn()
    {
        RandSpawnStep();
        RandPosStepSpawn();

        int temp = randStepOder % 2;
        if (temp == 1.25f)
        {
            Instantiate(stepList[randStepOder], new Vector2(randStepX + 2f, randStepY), Quaternion.identity);
        }
        else
        {
            Instantiate(stepList[randStepOder], new Vector2(randStepX - 2f, randStepY), Quaternion.identity);
        }
        
    }
    private void RandSpawnStep() => randStepOder = Random.Range(0, 3);
    private void RandPosStepSpawn()
    {
        if(gameplayDir == 0)
        {
            randStepX += 0.5f;
            if(randStepX == 2f)
            {
                gameplayDir = 1;
            }
        }else if(gameplayDir == 1)
        {
            randStepX -= 0.5f;
            if (randStepX == -2f)
            {
                gameplayDir = 0;

            }
        }
        randStepY += 0.75f;
        nextStepPos = new Vector3(randStepX, randStepY, 0);
    }
    private IEnumerator RisingTide()
    {
        yield return new WaitForSeconds(1);
        theTide.transform.position += new Vector3(0,0.1f,0);
        print("tide Pos: " + theTide.transform.position);
        StartCoroutine(RisingTide());
    }
    #endregion
    public void ToHome() => sceneMN.LoadScene(1, true);
    public void OnJump()
    {
        character.isJump = true;
        DecideStepSpawn();

        SettingNewTargetPos();
    }
    public void OnChangeDir() 
    {
        if(playerDir == 0)
        {
            playerDir = 1;
            character.CharDirAjuts(playerDir);
        }
        else if(playerDir == 1)
        {
            playerDir = 0;
            character.CharDirAjuts(playerDir);
        }
    }
    private void SettingNewTargetPos() => character.CaculatingNewTargetPos(new Vector3(nextStepPos.x - 0.25f, nextStepPos.y, 0));
    public void UpdateGameState(int state)
    {
        gameState = state;
        if(gameState == 2)
        {
            pausePnl.ShowPanel(true);
            StopAllCoroutines();
        }
    }
    public int CallbackGameState()
    {
        return gameState;
    }
}
