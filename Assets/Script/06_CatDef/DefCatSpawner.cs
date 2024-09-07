using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefCatSpawner : SpawnerSC
{
    [SerializeField] List<MouseAttackSC> enemies = new List<MouseAttackSC>();
    [SerializeField] CatDefMN mn;
    private int curLvl;
    private float delayTimeSpawn = 5f;
    private int mouseToSpawn;
    private void Start()
    {
        mn = GameObject.Find("DefCatManager").GetComponent<CatDefMN>();
        OnCheckLevel();
        DecideTimeSpawn(curLvl);
        StartCoroutine(DecideSpawnMouse(curLvl, delayTimeSpawn));
    }
    void SpawnMouse(int mouseToSpawn) => Instantiate(enemies[mouseToSpawn], transform.position, Quaternion.identity);
    private void Update() => OnMovement();
    private void OnCheckLevel() => curLvl = mn.curLvl;
    private IEnumerator DecideSpawnMouse(int curLvl, float delay)
    {
        //This gunction will decide which mouse will be spawn depend on current level
        yield return new WaitForSeconds(delay);
        SpawnMouse(mouseToSpawn);
        StartCoroutine(DecideSpawnMouse(curLvl,delay));
    }
    private void DecideTimeSpawn(int curLvl)
    {
        //Case of free and paid version
        if(curLvl >= 1 && curLvl <= 10)
        {
            if (curLvl == 1) delayTimeSpawn = 5f;
            else if(curLvl > 1 && curLvl <= 10)
            {
                delayTimeSpawn -= 0.2f;
            }
            mouseToSpawn = Random.Range(0, 3);
        }

        //Case of paid version
        if(mn.isPaid == true)
        {
            if(curLvl > 10 && curLvl <= 20)
            {

            }else if(curLvl > 20 && curLvl <= 40)
            {

            }else if(curLvl > 40 && curLvl <= 80)
            {

            }else if(curLvl > 80 && curLvl <= 120)
            {

            }else if(curLvl > 120 && curLvl <= 160)
            {

            }
        }
    }
    private void OnMovement() { }
}
