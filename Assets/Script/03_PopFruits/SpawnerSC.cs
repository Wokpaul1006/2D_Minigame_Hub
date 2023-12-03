using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSC : MonoBehaviour
{
    [Header("Variables")]
    private int _moveDir;
    private float _moveSpd;
    public bool _readySpawn;
    void Start() => SetUpStart();
    void SetUpStart() => _moveDir = 0;
    void Update() 
    {
        ChangeDir();
        Movementation(); 
    }
    private void Movementation()
    {

        //This function allow the spawner move linear
        if(_moveDir == -1 || _moveDir == 0)
        {
            transform.position += new Vector3(-1  * _moveSpd * Time.deltaTime, 0, 0);
        }else if(_moveDir == 1) 
        {
            transform.position += new Vector3(1  * _moveSpd * Time.deltaTime, 0, 0);
        }
    }
    public void SpeedUp(float level) 
    {
        //This function allow Spawner increase it's speed
        if (level == 1)
        {
            _moveSpd = level;
        }else if(level > 1)
        {
            _moveSpd = (level * 2)/2; 
        }
    } 
    private void ChangeDir()
    {
        if (transform.position.x <= -3)
        {
            _moveDir = 1;
        }
        else if(transform.position.x >= 3)
        {
            _moveDir = -1;
        }
    }
}
