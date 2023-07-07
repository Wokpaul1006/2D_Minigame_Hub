using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopFruitsManager : MonoBehaviour
{
    [SerializeField] List<GameObject> fruits = new List<GameObject>();
    [SerializeField] SpawnerSC spawner;
    [SerializeField] Transform parentCanvas;
    [SerializeField] Text scoreTxt, lvlTxt;

    private int rand, level, countSeconds, lvlMilestone, curScore;
    private float timeToSpawn;
    private Vector3 spawnerTrans;
    private void Start()
    {
        SetUpStart();
        StartCoroutine(CountingClock());
    }

    public void IncreaseScore()
    {
        curScore++;
        scoreTxt.text = curScore.ToString();
    }

    void SetUpStart()
    {
        level = 1;
        timeToSpawn = 1;
        lvlMilestone = 10;
        curScore = 0;
        spawner.SpeedUp(level);

        scoreTxt.text = curScore.ToString();
        lvlTxt.text = level.ToString();
    }

    private void RandFruitToSpawn()
    {
        rand = Random.Range(0, fruits.Count);
    }

    private void GetSpawnerPos()
    {
        spawnerTrans = spawner.transform.position;
    }
    private void OnSpawnFruits()
    {
        RandFruitToSpawn();
        GetSpawnerPos();
        Instantiate(fruits[rand], spawnerTrans, Quaternion.identity, parentCanvas);
    }
    private IEnumerator CountingClock()
    {
        yield return new WaitForSeconds(1f);
        countSeconds++;
        if(countSeconds == lvlMilestone)
        {
            lvlMilestone = countSeconds + 10;
            OnLevelUp();
        }
        WaitToSpawn(timeToSpawn);
        StartCoroutine(CountingClock());
    }
    private void WaitToSpawn(float waitTime)
    {
        OnSpawnFruits();
        Invoke("WaitToSpawn", waitTime);
    }
    private void OnLevelUp()
    {
        level++;
        timeToSpawn = timeToSpawn / 10f;
        spawner.SpeedUp(level);

        lvlTxt.text = level.ToString();
    }

    public void BackHome()
    {
        SceneManager.LoadScene("00_Home");
    }
}
