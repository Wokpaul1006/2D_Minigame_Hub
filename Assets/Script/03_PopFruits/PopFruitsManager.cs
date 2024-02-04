using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;

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
    [HideInInspector] public int missedFruits;
    [SerializeField] List<GameObject> cloundList = new List<GameObject>();

    private int rand, level, curScore;
    private int nextLvlTarget;
    private int randBird;
    private float  delaySpawnTime, baseDelayTime; //This variable base on current level
    private void Start()
    {
        DecideToSpawnBirds();
        SettingStart();

        #region Countdown Start Handle
        if (coundownNumber == 5 && coundownNumber >= 0) StartCoroutine(StartCoundown());
        else if (coundownNumber == 0 || coundownNumber <= 0) StopCoroutine(StartCoundown());
        #endregion

        DecideDelaySpawn(level);
        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    void SettingStart()
    {
        UpdateGameState(0);//Idle
        UpdateLevel(1);

        curScore = 0;
        coundownNumber = 5;
        baseDelayTime = 5;
        nextLvlTarget = 10;

        UpdateLvlText(level);
        UpdateTextScore(curScore);
        UpdateMissedFruitText();

        StartCoroutine(SpawnClound());
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
            StopCoroutine(StartCoundown());

        }
        StartCoroutine(StartCoundown());
    }
    #endregion

    #region Handle UIs
    public void ToHome()
    {
        StopAllCoroutines();
        sceneMN.LoadScene(1, true);
    }
    private void UpdateLvlText(int lvl) => lvlTxt.text = lvl.ToString();
    private void UpdateTextScore(int curScoreing) => scoreTxt.text = curScoreing.ToString();
    private void UpdateMissedFruitText() => lostFruits.text = missedFruits.ToString();
    #endregion

    #region Handle gamplay & logics
    private IEnumerator SpawnClound()
    {
        yield return new WaitForSeconds(2);
        int randY = Random.Range(-3, 3);
        Instantiate(cloundList[Random.Range(0, 2)], new Vector3(4, randY, 0), Quaternion.identity);
        StartCoroutine(SpawnClound());
    }
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
        switch (gameState)
        {
            case 1:
                OnCheckLevel(level);
                coundonwPanel.SetActive(false);
                break;
            case 2:
                ShowPause();
                StopAllCoroutines();
                break;
        }
    }
    public void ShowPause()
    {
        pausePnl.ShowPanel(true);
        StopAllCoroutines();
    }
    private void OnCheckLevel(int curLvl)
    {
        if(curLvl != 1)
        {
            birdsList[randBird].GetComponent<SpawnerSC>().SpeedUp(level, delaySpawnTime);
            DecideDelaySpawn(level);
            UpdateLvlText(level); //Update text
            UpdateLevel(level); //Update of ingame
            UpdtatePlayerPrefs(); 
        }
        else
        {
            DecideDelaySpawn(level);
            birdsList[randBird].GetComponent<SpawnerSC>().SpeedUp(level, delaySpawnTime);
        }
    }
    public void CountMiss()
    {
        missedFruits++;
        UpdateMissedFruitText();
        if (missedFruits >= 10)
        {
            StopAllCoroutines();
            pausePnl.ShowPanel(true);
        }
    }
    private void DecideNextLevelTarget() => nextLvlTarget = (level * 10) + level * 2;
    public void CountScore()
    {
        curScore++;
        UpdateTextScore(curScore);
        if(curScore == nextLvlTarget) 
        {
            level++;
            DecideNextLevelTarget(); //Once player reach the target score of current Lvl, this functin will active to caculate the new target.
            OnCheckLevel(level); //After the new target is caculated, run this function to update whole gameplay
        }
    }
    #endregion

    #region Spawning Fruits
    private void DecideDelaySpawn(int lvl)
    {
        if (lvl == 1) delaySpawnTime = baseDelayTime;
        else if (lvl > 1 && level <= 21) delaySpawnTime -= 0.1f;
        else lvl = Random.Range(2, 5);
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
