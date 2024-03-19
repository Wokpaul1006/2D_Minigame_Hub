using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class CrossLaneSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> carList = new List<GameObject>();
    [HideInInspector] CrosslaneManager mn = new CrosslaneManager();    
    [HideInInspector] float delaySpawn; //This variable will change in odds ratio with level by decrease 0.1 per level
    [HideInInspector] int baseDelay = 1;
    [HideInInspector] Vector3 spawnPos;

    private int currentLvl;
    private int randCar = 0;
    private int carDir;

    private void Start()
    {
        SetupStart();
        mn = GameObject.Find("CrossLaneManager").GetComponent<CrosslaneManager>();
    }
    void SetupStart()
    {
        delaySpawn = baseDelay;
        spawnPos = gameObject.transform.position;
        if (gameObject.name == "OBJ_Spawner_Left01(Clone)") carDir = 1;
        else if (gameObject.name == "OBJ_Spawner_Right01(Clone)") carDir = -1;
        StartCoroutine(WaitToSpawnCar());
    }
    private void SpawnCar()
    {
        if(carDir == 1) Instantiate(carList[DecideRandCar()], spawnPos, Quaternion.Euler(0, 180, 90));
        else if (carDir == -1) Instantiate(carList[DecideRandCar()], spawnPos, Quaternion.Euler(0, 180, -90));
    }
    private int DecideRandCar() { return randCar = Random.Range(1, carList.Count); }
    IEnumerator WaitToSpawnCar() 
    {
        OnRefresherLevel();
        if (currentLvl <= 10)
        {   delaySpawn = Random.Range(7, 10); }
        else if(currentLvl >10 && currentLvl <= 20)
        {   delaySpawn = Random.Range(4, 7.5f); }
        else if(currentLvl > 20)
        { delaySpawn = Random.Range(4, 10f); }
        yield return new WaitForSeconds(delaySpawn);
        SpawnCar();
        StartCoroutine(WaitToSpawnCar());
    }
    void OnRefresherLevel() => currentLvl = mn.curLvl;
}
