using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossLaneSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> carList = new List<GameObject>();

    [HideInInspector] float delaySpawn; //This variable will change in odds ratio with level by decrease 0.1 per level
    [HideInInspector] int baseDelay = 1;
    [HideInInspector] Vector3 spawnPos;

    private int randCar;

    private void Start()
    {
        SetupStart();
        SpawnCar();
        StartCoroutine(WaitToSpawnCar(delaySpawn));
    }
    void SetupStart()
    {
        delaySpawn = baseDelay;
        randCar = 0;
        spawnPos = gameObject.transform.position;
    }

    private void SpawnCar() => Instantiate(carList[DecideRandCar()], spawnPos, Quaternion.identity);
    private int DecideRandCar() { return randCar = Random.Range(1, carList.Count); }
    IEnumerator WaitToSpawnCar(float spawnTime) 
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnCar();
        StartCoroutine(WaitToSpawnCar(delaySpawn));
    }
    public void UpdateDelayTime(int curLevel)
    {
        //Call this function any time the level update
        delaySpawn -= 0.1f;
        if(delaySpawn <= 0.2f)
        {
            delaySpawn = 0.2f;
        }

    }

}
