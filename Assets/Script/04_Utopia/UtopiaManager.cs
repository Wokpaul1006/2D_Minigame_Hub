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

    //Common zone
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;

    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;

    //Specific zone
    [SerializeField] List<GameObject> stepList = new List<GameObject>();

    private int gameState; //Show state of the game. 0 is idle, 1 is in-play
    private int baseScore = 0;
    private int baseLevel = 1;
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

        randStepY = -2;
        randStepX = 0;
        DecideStepSpawn();
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
        print(randStepX);
        if(randStepX <= 0 && randStepX > 2)
        {
            print("increase X");
            randStepX += 0.5f;
        }else if(randStepX > 0 && randStepX < 2)
        {
            print("decrease X");
            randStepX -= 0.5f;
        }

        randStepY += 0.5f;
    }    
    #endregion
    public void ToHome() => sceneMN.LoadScene(1, true);
    public void OnJump()
    {
        DecideStepSpawn();
    }
}
