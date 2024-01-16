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
    [SerializeField] GameObject ground;
    [SerializeField] List<GameObject> cloundList = new List<GameObject>();
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

        DecideTimeSpawn(curLevel);
        StartCoroutine(OnWaitToSpawnObstacle(delaySpawnTime));
        StartCoroutine(SpawnClound());
        StartCoroutine(GroundMovement());

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
            StopCoroutine(StartCoundown());
        }
        StartCoroutine(StartCoundown());
    }
    #region UIs Handlers
    private void HandleCoundownStartText() => startCoundownTxt.text = coundownNumber.ToString();
    private void UpdateOnScreenSecond() => currentTimeSurvive.text = timeSurvive.ToString() + "s";
    private void IncreaseUIScore() => curretScore.text = ingameScore.ToString();
    private void IncreaseUILevel() => currentLevel.text = curLevel.ToString();
    public void ShowPause()
    {
        pausePnl.ShowPanel(true);
        StopAllCoroutines();
    }
    #endregion

    #region Gameplay Handlers
    private IEnumerator SpawnClound()
    {
        yield return new WaitForSeconds(2);
        int randY = Random.Range(-1, 5);
        Instantiate(cloundList[Random.Range(0, 2)], new Vector3(4, randY, 0), Quaternion.identity);
        StartCoroutine(SpawnClound());
    }
    private IEnumerator GroundMovement()
    {
        yield return new WaitForSeconds(0.1f);
        if(gameStage == 1)
        {
            if (ground.transform.position.x <= -8)
            {
                ground.transform.position = new Vector3(8, -4, 0);
            }
            else
            {
                ground.transform.position += Vector3.left;
            }
        }else if(gameStage == 2)
        {
            StopCoroutine(GroundMovement());
        }
        StartCoroutine(GroundMovement());
    }
    private void DecideTimeSpawn(int lvl)
    {
        if (lvl == 1)
            delaySpawnTime = 5f;
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
    private void SpawnObjects()
    {
        objApparance = Random.Range(0, listObts.Count);
        Instantiate(listObts[objApparance], spawnerPos, Quaternion.identity);
    }
    private IEnumerator OnWaitToSpawnObstacle(float delay)
    {
        yield return new WaitForSeconds(delaySpawnTime);

        objApparance = Random.Range(0, listObts.Count);
        Instantiate(listObts[objApparance], spawnerPos, Quaternion.identity);

        StartCoroutine(OnWaitToSpawnObstacle(delay));
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
            IncreaseUILevel();

            UpdtatePlayerPrefs();
        }
        UpdateOnScreenSecond();
        //GroundMovement();
    }

    private void UpdtatePlayerPrefs()
    {
        //Get player prefs section
        int currenTotalScore;
        int highestScoreToCompare;
        int highestLevelToCompare;

        int newTotalScore;

        currenTotalScore = PlayerPrefs.GetInt("PTotalScore");
        highestLevelToCompare = PlayerPrefs.GetInt("PHighestLevel");
        highestScoreToCompare = PlayerPrefs.GetInt("PHighestScore");

        //Update total score
        newTotalScore = currenTotalScore + ingameScore;
        PlayerPrefs.SetInt("PTotalScore", ingameScore); //total of score that player have earn

        //Update highest score
        if(highestScoreToCompare < ingameScore) PlayerPrefs.SetInt("PHighestScore", ingameScore); //highest score that player can reach of all games

        //Update highets level
        if (highestLevelToCompare < curLevel) PlayerPrefs.SetInt("PHighestLevel", curLevel); //highest level player can reach
    }
    #endregion
}
