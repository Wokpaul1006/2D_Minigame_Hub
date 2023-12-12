using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class JumpManager : MonoBehaviour
{
    //No. of Game" #01
    //Rule:
    //Player jump to avoid obstacles, ì hit obstacle, game end.

    //Common Zone
    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;
    [SerializeField] Text startCoundownTxt;
    [SerializeField] GameObject coundonwPanel;
    private int coundownNumber;
    private int gameStage;

    //Sepcific Zone
    [SerializeField] JumpDinoSC dino;
    [SerializeField] Transform gamezone;
    [SerializeField] List<GameObject> listObts = new List<GameObject>();
    [SerializeField] Text currentTimeSurvive;
    [SerializeField] GameObject obstacleSpawner;

    private Vector3 spawnerPos;
    private int objApparance;
    private int ingameScore;
    private int timeSurvive;
    private int curLevel;
    private int nextLvlTarget;
    private float delaySpawnTime;
    void Start()
    {
        ingameScore = 0;
        timeSurvive = 0;
        curLevel = 1;
        nextLvlTarget = 10;
        coundownNumber = 5;
        UpdateGameState(0);

        #region Countdown Start
        HandleCoundownStartText();
        if (coundownNumber == 5 && coundownNumber >= 0) StartCoroutine(StartCoundown());
        else if (coundownNumber == 0 || coundownNumber <= 0) StopCoroutine(StartCoundown());
        #endregion

        currentLevel.text = 1.ToString();
        curretScore.text = 0.ToString();
        spawnerPos = obstacleSpawner.transform.position;

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    private IEnumerator StartCoundown()
    {
        yield return new WaitForSeconds(1);
        coundownNumber--;
        startCoundownTxt.text = coundownNumber.ToString();
        if (coundownNumber <= 0)
        {
            UpdateGameState(1);
        }
        StartCoroutine(StartCoundown());
    }
    #region UIs Handlers
    private void HandleCoundownStartText() => startCoundownTxt.text = coundownNumber.ToString();
    private void UpdateOnScreenSecond() => currentTimeSurvive.text = timeSurvive.ToString() + "s";
    private void IncreaseUIScore() => curretScore.text = ingameScore.ToString();
    public void ShowPause()
    {
        pausePnl.ShowPanel(true);
        StopAllCoroutines();
    }
    #endregion

    #region Gameplay Handlers
    private void DecideTimeSpawn(int lvl)
    {
        if (lvl == 1)
            delaySpawnTime = 4f;
        else if (lvl > 1 && lvl <= 21)
        {
            delaySpawnTime -= 0.1f;
        }
        else lvl = Random.Range(2, 5);
    }
    public void UpdateGameState(int state)
    {
        gameStage = state;
        switch(gameStage) 
        {
            case 1:
                DecideNextLevelTarget();
                coundonwPanel.SetActive(false);
                StartCoroutine(CountToScore());
                StartCoroutine(OnWaitToSpawnObstacle());
                break;
            case 2:
                ShowPause();
                StopAllCoroutines();
                break;
        }
    }
    private void DecideNextLevelTarget() => nextLvlTarget = (curLevel * 10);
    private void IncreaseInGameScore() => ingameScore++;
    private void IncreaseInGameLevel() => curLevel++;
    #endregion

    #region Handle Gameplay Activities
    public void OnJump()
    {
        if (dino.isGrounded == true)
        {
            dino.isGrounded = false;
            dino.allowJump = true;
        }
    }
    private IEnumerator OnWaitToSpawnObstacle()
    {
        yield return new WaitForSeconds(5);
        SpawnObstacle();
        //StartCoroutine(OnWaitToSpawnObstacle());
    }
    private void SpawnObstacle()
    {
        objApparance = Random.Range(0, listObts.Count);
        Instantiate(listObts[objApparance], spawnerPos, Quaternion.identity);
    }
    private IEnumerator CountToScore()
    {
        yield return new WaitForSeconds(1);
        timeSurvive++;
        IncreaseInGameScore();
        IncreaseUIScore();
        if (ingameScore == nextLvlTarget)
        {
            IncreaseInGameLevel();
            DecideTimeSpawn(curLevel);
            DecideNextLevelTarget();
        }
        UpdateOnScreenSecond();
    }
    #endregion
}
