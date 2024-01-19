using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSC : MonoBehaviour
{
    [Header("Variables")]
    private int _moveDir;
    private float _moveSpd;
    private int randF;
    private int curLvl;

    [SerializeField] List<GameObject> fruitsList = new List<GameObject>();
    Vector3 curPos;
    void Start() => SetUpStart();
    void SetUpStart()
    {
        _moveDir = 0;
        StartCoroutine(OnSpawnFruits());
    }
    void Update() => Movementation();
    private void Movementation()
    {
        //This function allow the spawner move linear
        if (_moveDir == 1 || _moveDir == 0) transform.position += new Vector3(1  * Time.deltaTime, 0, 0);
        else if (_moveDir == -1) transform.position += new Vector3(-1  * Time.deltaTime, 0, 0);

        GetCurPos();
        ChangeDir();
    }
    public void SpeedUp(float level) 
    {
        //This function allow Spawner increase it's speed
        if (level == 1) _moveSpd = level*10;
        else if(level > 1) _moveSpd = ((level * 2) / 2)*10;
    } 
    private void ChangeDir()
    {
        if (transform.position.x <= -2.5)
        {
            _moveDir = 1;
            transform.localScale = new Vector3(transform.localScale.x * -1, 0.25f, 0.25f);
        }
        else if (transform.position.x >= 2.5)
        {
            _moveDir = -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, 0.25f, 0.25f);
        }
    }
    public void SpawnFruits() => Instantiate(fruitsList[RandFruitToSpawn()], new Vector3(curPos.x, curPos.y - 0.5f, 0), Quaternion.identity);
    private int RandFruitToSpawn()
    {
        randF = Random.Range(0, fruitsList.Count);
        return randF;
    }
    private void GetCurPos() => curPos = transform.position;
    private IEnumerator OnSpawnFruits()
    {
        yield return new WaitForSeconds(1);
        SpawnFruits();
        StartCoroutine(OnSpawnFruits());
    }
}
