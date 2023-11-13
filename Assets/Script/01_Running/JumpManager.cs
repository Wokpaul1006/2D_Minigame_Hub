using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JumpManager : MonoBehaviour
{
    [SerializeField] JumpDinoSC dino;
    [SerializeField] JumpOstacles objs;
    [SerializeField] Transform gamezone;
    [SerializeField] PauseSC pausePnl;
    [SerializeField] Text currentTimeSurvive;
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;
    [SerializeField] List<Sprite> objImage = new List<Sprite>();

    private int objImageIndex;
    private int ingameScore;
    private int timeSurvive;
    private int curLevel;
    private int nextLvlTarget;
    // Start is called before the first frame update
    void Start()
    {
        ingameScore = 0;
        timeSurvive = 0;
        curLevel = 1;
        nextLvlTarget = 10;

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
        currentLevel.text = 1.ToString();
        curretScore.text = 0.ToString();

        SpawnDino();
        DecideNextLevelTarget();
        StartCoroutine(OnWaitToSpawnObstacle());
        StartCoroutine(CountToScore());
    }

    private void SpawnDino() => dino = Instantiate(dino, gamezone.parent);
    private IEnumerator OnWaitToSpawnObstacle()
    {
        yield return new WaitForSeconds(5);
        SpawnObstacle();
        StartCoroutine(OnWaitToSpawnObstacle());
    }
    private void SpawnObstacle()
    {
        objImageIndex = Random.Range(0, objImage.Count);
        Instantiate(objs, gamezone.parent);
        objs.SetApparance(objImage[objImageIndex]);
    }
    public void OnJump() => dino.EnableJump();
    public void ShowPause()
    {
        pausePnl.ShowPanel(true);
        StopAllCoroutines();
    }
    private IEnumerator CountToScore()
    {
        yield return new WaitForSeconds(1);
        timeSurvive++;
        IncreaseInGameScore();
        IncreaseUIScore();
        if(ingameScore == nextLvlTarget)
        {
            IncreaseInGameLevel();
            DecideNextLevelTarget();
        }
        UpdateOnScreenSecond();
        StartCoroutine(CountToScore());
    }
    private void UpdateOnScreenSecond() => currentTimeSurvive.text = timeSurvive.ToString()+"s";
    private void IncreaseInGameScore() => ingameScore++;
    private void IncreaseInGameLevel() => curLevel++;
    private void IncreaseUIScore() => curretScore.text = ingameScore.ToString();
    private void DecideNextLevelTarget()
    {
        nextLvlTarget = (curLevel * 10);
    }
    public void ToHome() => SceneManager.LoadScene("00_Home");
}
