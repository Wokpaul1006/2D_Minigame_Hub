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
    //Player pop the fruit fall from the spawner, if miss more tha 5 fruist, game end.

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
    [SerializeField] SpawnerSC spawner;
    [SerializeField] Transform parent;
    [SerializeField] Text lostFruits;
    [SerializeField] List<GameObject> fruits = new List<GameObject>();
    [HideInInspector] public int missedFruits;

    private int rand, level, countSeconds, lvlMilestone, curScore;
    private float delaySpawnTime, baseDelayTime; //This variable base on current level
    private Vector3 spawnerTrans;
    private void Start()
    {
        SettingStart();

        #region Countdown Start Handle
        if (coundownNumber == 5 && coundownNumber >= 0) StartCoroutine(StartCoundown());
        else if (coundownNumber == 0 || coundownNumber <= 0) StopCoroutine(StartCoundown());
        #endregion

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
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
    private void UpdateLvlText(int lvl) => lvlTxt.text = lvl.ToString();
    private void UpdateTextScore(int curScoreing) => scoreTxt.text = curScoreing.ToString();
    private void UpdateMissedFruitText() => lostFruits.text = missedFruits.ToString();
    #endregion

    #region Handle gamplay logics
    void UpdateLevel(int lvl) => level = lvl;
    public void UpdateGameState(int state)
    {
        gameState = state;
        if (gameState == 2)
        {
            pausePnl.ShowPanel(true);
            StopAllCoroutines();
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
        GetSpawnerPos();
        Instantiate(fruits[rand], spawnerTrans, Quaternion.identity);
        Invoke("OnSpawnFruits", delaySpawnTime);
    }
    private void RandFruitToSpawn() => rand = Random.Range(0, fruits.Count);
    private void GetSpawnerPos() => spawnerTrans = spawner.transform.position;
    private void DecideDelaySpawn()
    {
        if (level == 1)
            delaySpawnTime = baseDelayTime;
        else
            delaySpawnTime -= 0.1f;
    }
    #endregion

    private void OnCheckLevel()
    {
        if(level == 1)
        {
            DecideDelaySpawn();
            spawner.SpeedUp(level);
        }
        else
        {
            level++;
            spawner.SpeedUp(level);
            DecideDelaySpawn();
            UpdateLvlText(level);
        }
    }
    public void CountMiss()
    {
        missedFruits++;
        UpdateMissedFruitText();
        if(missedFruits >= 10)
        {
            pausePnl.ShowPanel(true);
        }
    }
    public void CountSocre() 
    {
        curScore++;
        UpdateTextScore(curScore); 
        if(curScore >= 10)
        {
            OnCheckLevel();
        }
    }
    public void ToHome() => sceneMN.LoadScene(1, true);
}
