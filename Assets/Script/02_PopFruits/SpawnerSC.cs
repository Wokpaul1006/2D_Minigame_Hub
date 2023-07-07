using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSC : MonoBehaviour
{
    [Header("Variables")]
    private int _moveDir;
    private float _moveSpd;
    public bool _readySpawn;
    void Start()
    {
        SetUpStart();
    }
    void SetUpStart() => _moveDir = 0;
    void Update()
    {
        Movementation();
    }
    private void Movementation()
    {
        if(_moveDir == -1 || _moveDir == 0)
        {
            transform.position += new Vector3(-1 * Time.deltaTime * _moveSpd, 0, 0);
        }else if(_moveDir == 1) 
        {
            transform.position += new Vector3(1 * Time.deltaTime * _moveSpd, 0, 0);
        }        
    }
    public void SpeedUp(float level)
    {
        _moveSpd = level * 2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "LEdge")
        {
            _moveDir = 1;
        }else if(collision.gameObject.tag == "REdge")
        {
            _moveDir = -1;
        }
    }
}
