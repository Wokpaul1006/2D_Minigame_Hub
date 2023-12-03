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
    private float timeToSpawn;
    private Vector3 spawnerTrans;
    private void Start()
    {
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

        timeToSpawn = 10;
        lvlMilestone = 10;
        curScore = 0;
        coundownNumber = 5;

        spawner.SpeedUp(level);
        lvlTxt.text = level.ToString();

        UpdateTextScore();
        UpdateMissedFruitText();

        //pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
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
            StartCoroutine(CountingClock());
        }
        StartCoroutine(StartCoundown());
    }
    #endregion

    #region Handle UIs

    private void UpdateTextScore() => scoreTxt.text = curScore.ToString();
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
    private void OnSpawnFruits()
    {
        RandFruitToSpawn();
        GetSpawnerPos();
        Instantiate(fruits[rand], spawnerTrans, Quaternion.identity, parent);
    }
    private void RandFruitToSpawn() => rand = Random.Range(0, fruits.Count);
    private void GetSpawnerPos() => spawnerTrans = spawner.transform.position;
    #endregion

    private IEnumerator CountingClock()
    {
        yield return new WaitForSeconds(1f);
        countSeconds++;
        WaitoSpawn(timeToSpawn);
        if(countSeconds == lvlMilestone)
        {
            lvlMilestone = countSeconds + 10;
            OnLevelUp();
        }
        StartCoroutine(CountingClock());
    }
    IEnumerator WaitoSpawn(float waitTime)
    {
        print("in spawn");
        yield return new WaitForSeconds(waitTime);
        OnSpawnFruits();
    }
    private void OnLevelUp()
    {
        level++;
        timeToSpawn = (timeToSpawn * level)/10;
        spawner.SpeedUp(level);

        lvlTxt.text = level.ToString();
    }
    public void CountMiss()
    {
        missedFruits++;
        UpdateMissedFruitText();
        if(missedFruits >= 10)
        {
            //pausePnl.ShowPanel(true);
        }
    }
    public void CountSocre() 
    {
        curScore++;
        UpdateTextScore(); 
    }
    public void ToHome() => sceneMN.LoadScene(1, true);
}
