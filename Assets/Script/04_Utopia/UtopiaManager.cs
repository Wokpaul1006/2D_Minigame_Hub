using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class UtopiaManager : MonoBehaviour
{
    //No. of Game" #03
    //Rule:
    //Player jump to reach new footstep, if fall 3 time, game end.
    //Every time player hit new footstep, plus one player score.

    //Common zone
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;
    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;

    //Specific zone
    [SerializeField] List<GameObject> stepList = new List<GameObject>();
    [SerializeField] UtopiaCharSC character;
    [HideInInspector] Vector3 startPos = new Vector3(-1,0, 0);
    [HideInInspector] Vector3 nextStepPos;

    private int gameState; //Show state of the game. 0 is idle, 1 is in-play
    private int baseScore = 0;
    private int baseLevel = 1;
    private int gameplayDir; //Direction of both stepfoot and player. 0 = head 2, 1 = head -2
    private int randStepOder; //Random oder of footstep in the list
    private float randStepX, randStepY;
    void Start()
    {
        SettingStart();
        HandleUIs();

        //pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    private void SettingStart()
    {
        gameState = 0;
        baseScore = 0;
        baseLevel = 0;

        randStepY = -3;
        randStepX = 0;

        gameplayDir = 0;
        DecideStepSpawn();
        SpawnPlayer();
    }

    #region Internal Handle
    private void HandleUIs()
    {
        curretScore.text = baseScore.ToString();
        currentLevel.text = baseLevel.ToString();
    }
    #endregion

    #region Gameplay Handle
    private void DecideStepSpawn()
    {
        RandSpawnStep();
        RandPosStepSpawn();

        Instantiate(stepList[randStepOder], new Vector2(randStepX, randStepY), Quaternion.identity);
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
            if (gameplayDir == -2f)
            {
                gameplayDir = 0;
            }
        }
        randStepY += 0.5f;
        nextStepPos = new Vector3(randStepX, randStepY, 0);
    }    
    private void SpawnPlayer() => character = Instantiate(character, startPos, Quaternion.identity);
    #endregion
    public void ToHome() => sceneMN.LoadScene(1, true);
    public void OnJump()
    {
        character.isJump = true;
        DecideStepSpawn();
    }
    private void SettingNewTargetPos()
    {
        character.CaculatingNewTargetPos(nextStepPos);
    }
}
