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

    private IEnumerator OnWaitToSpawnObstacle()
    {
        yield return new WaitForSeconds(5);
        SpawnObstacle();
        StartCoroutine(OnWaitToSpawnObstacle());
    }
    private void SpawnObstacle()
    {
        objApparance = Random.Range(0, listObts.Count);
        Instantiate(listObts[objApparance], spawnerPos, Quaternion.identity);
    }
    public void OnJump()
    {
        if(dino.isGrounded == true)
        {
            dino.isGrounded = false;
            dino.allowJump = true;
        }
    }
    public void ShowPause()
    {
        pausePnl.ShowPanel(true);
        StopAllCoroutines();
    }
    private void HandleCoundownStartText() => startCoundownTxt.text = coundownNumber.ToString();
    private IEnumerator CountToScore()
    {
        yield return new WaitForSeconds(1);
        timeSurvive++;
        IncreaseInGameScore();
        IncreaseUIScore();
        if(ingameScore == nextLvlTarget)
        {
            IncreaseInGameLevel();
            DecideTimeSpawn(curLevel);
            DecideNextLevelTarget();
        }
        UpdateOnScreenSecond();
        StartCoroutine(CountToScore());
    }
    private void UpdateOnScreenSecond() => currentTimeSurvive.text = timeSurvive.ToString()+"s";
    private void IncreaseInGameScore() => ingameScore++;
    private void IncreaseInGameLevel() => curLevel++;
    private void IncreaseUIScore() => curretScore.text = ingameScore.ToString();
    private void DecideNextLevelTarget() => nextLvlTarget = (curLevel * 10);
    public void ToHome() => sceneMN.LoadScene(1, true);
    private IEnumerator StartCoundown()
    {
        yield return new WaitForSeconds(1);
        coundownNumber--;
        startCoundownTxt.text = coundownNumber.ToString();
        if (coundownNumber <= 0)
        {
            coundonwPanel.SetActive(false);
            DecideNextLevelTarget();
            StartCoroutine(OnWaitToSpawnObstacle());
            StartCoroutine(CountToScore());
        }
        StartCoroutine(StartCoundown());
    }
    private void DecideTimeSpawn(int lvl)
    {
        if (lvl == 1)
            delaySpawnTime = 4f;
        else if(lvl >1 && lvl <= 21)
        {
            delaySpawnTime -= 0.1f;
        }
        else lvl = Random.Range(2, 5);
    }
}
