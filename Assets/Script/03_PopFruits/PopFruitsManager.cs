using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class PopFruitsManager : MonoBehaviour
{
    //No. of Game" #02
    //Rule:
    //Player pop the eggs fall from the spawner, if miss more than 5 eggs, game end.

    //Common zone
    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;
    [SerializeField] Text scoreTxt;
    [SerializeField] Text lvlTxt;
    [SerializeField] Text startCoundownTxt;
    [SerializeField] GameObject coundonwPanel;
    private int coundownNumber;
    private int gameState;

    //Specific Zone
    [SerializeField] AudioSource popSFX;
    [SerializeField] Transform parent;
    [SerializeField] Text lostFruits;
    [SerializeField] List<GameObject> birdsList = new List<GameObject>();
    [SerializeField] List<GameObject> fruitsList = new List<GameObject>();
    [HideInInspector] public int missedFruits;

    private int rand, level, countSeconds, lvlMilestone, curScore;
    private int randBird;
    private float delaySpawnTime, baseDelayTime; //This variable base on current level
    private void Start()
    {
        DecideToSpawnBirds();
        SettingStart();

        #region Countdown Start Handle
        if (coundownNumber == 5 && coundownNumber >= 0) StartCoroutine(StartCoundown());
        else if (coundownNumber == 0 || coundownNumber <= 0) StopCoroutine(StartCoundown());
        #endregion

        //pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    void SettingStart()
    {
        UpdateGameState(0);//Idle
        UpdateLevel(1);

        delaySpawnTime = 10;
        lvlMilestone = 10;
        curScore = 0;
        coundownNumber = 5;
        baseDelayTime = 5;

        UpdateLvlText(level);
        UpdateTextScore(curScore);
        UpdateMissedFruitText();
    }
    #region Handle Start Countdown
    private IEnumerator StartCoundown()
    {
        yield return new WaitForSeconds(1);
        coundownNumber--;
        startCoundownTxt.text = coundownNumber.ToString();
        if (coundownNumber <= 0)
        {
            coundonwPanel.SetActive(false);
            UpdateGameState(1);
            StartGameplay();
        }
        StartCoroutine(StartCoundown());
    }
    #endregion
    
    #region Handle UIs
    public void ToHome() => sceneMN.LoadScene(1, true);
    private void UpdateLvlText(int lvl) => lvlTxt.text = lvl.ToString();
    private void UpdateTextScore(int curScoreing) => scoreTxt.text = curScoreing.ToString();
    private void UpdateMissedFruitText() => lostFruits.text = missedFruits.ToString();
    #endregion

    #region Handle gamplay & logics
    private void DecideToSpawnBirds()
    {
        randBird = Random.Range(0, birdsList.Count);
        Instantiate(birdsList[randBird], new Vector3(0, 2, 0), Quaternion.identity);
    }
    public void PlayPopSound() => popSFX.Play();
    private void UpdateLevel(int lvl) => level = lvl;
    public void UpdateGameState(int state)
    {
        gameState = state;
        if (gameState == 2)
        {
            pausePnl.ShowPanel(true);
            StopAllCoroutines();
        }
    }
    private void OnCheckLevel()
    {
        if (level == 1)
        {
            DecideDelaySpawn();
            birdsList[randBird].GetComponent<SpawnerSC>().SpeedUp(level);
        }
        else
        {
            level++;
            birdsList[randBird].GetComponent<SpawnerSC>().SpeedUp(level);
            DecideDelaySpawn();
            UpdateLvlText(level);
            UpdtatePlayerPrefs();
        }
    }
    public void CountMiss()
    {
        missedFruits++;
        UpdateMissedFruitText();
        if (missedFruits >= 10) pausePnl.ShowPanel(true);
    }
    public void CountScore()
    {
        curScore++;
        UpdateTextScore(curScore);
        if (curScore >= 10)
        {
            OnCheckLevel();
        }
    }
    #endregion

    #region Spawning Fruits
    void StartGameplay()
    {
        if(gameState == 1)
        {
            OnSpawnFruits();
            OnCheckLevel();
        }
    }
    private void OnSpawnFruits()
    {
        RandFruitToSpawn();
        Instantiate(fruitsList[rand], birdsList[randBird].transform.position, Quaternion.identity);
        Invoke("OnSpawnFruits", delaySpawnTime);
    }
    private void RandFruitToSpawn() => rand = Random.Range(0, fruitsList.Count);
    private void DecideDelaySpawn()
    {
        if (level == 1)
            delaySpawnTime = baseDelayTime;
        else
            delaySpawnTime -= 0.1f;
    }
    #endregion

    #region Backend Activities
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
        newTotalScore = currenTotalScore + curScore;
        PlayerPrefs.SetInt("PTotalScore", curScore); //total of score that player have earn

        //Update highest score
        if (highestScoreToCompare < curScore) PlayerPrefs.SetInt("PHighestScore", curScore); //highest score that player can reach of all games

        //Update highets level
        if (highestLevelToCompare < level) PlayerPrefs.SetInt("PHighestLevel", level); //highest level player can reach
    }
    #endregion
}
