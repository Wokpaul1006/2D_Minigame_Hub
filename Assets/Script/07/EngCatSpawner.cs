using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngCatSpawner : SpawnerSC
{
    private int curLvl;
    private int spawnerDesignature; //0 is left and 1 is right
    private EngCatMN manager;
    [SerializeField] List<GameObject> mouse = new List<GameObject>();
    void Start()
    {
        manager = GameObject.Find("EngCatManager").GetComponent<EngCatMN>();
        SettingStart();
        RefreshByLevel();
    }

    private void SettingStart()
    {
       curLvl = manager.curLvl;
    }

    internal void RefreshByLevel()
    {
        if (curLvl == 1)
        {
            //On Init Game case
            if (spawnerDesignature == 0) SpawnObstacleLeftToRight();
            else if (spawnerDesignature == 1) SpawnObstacleRightToLeft();
        }
        else
        {
            //On current process game case
            if (spawnerDesignature == 0) SpawnObstacleLeftToRight();
            else if (spawnerDesignature == 1) SpawnObstacleRightToLeft();
        }
    }
    private void SpawnObstacleLeftToRight()
    {
        Instantiate(mouse[0], transform.position, Quaternion.Euler(0, 0, 90));
    }
    private void SpawnObstacleRightToLeft()
    {
        Instantiate(mouse[0], transform.position, Quaternion.Euler(0, 0, -90));
    }
    private void DecideWhichMouseWillSpawnAtAMoment()
    {

    }
    private void DetectSpawner()
    {
        if (gameObject.name == "OBJ_SpawnerRight") spawnerDesignature = 1;
        else if (gameObject.name == "OBJ_Spawnerleft") spawnerDesignature = 0;
    }
}
