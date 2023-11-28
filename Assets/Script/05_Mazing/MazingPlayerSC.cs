using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class MazingPlayerSC : MonoBehaviour
{
    public Vector3 playerPos;
    void Start()
    {
        
    }
    void Update()
    {

    }
    public void CharMove(int dir)
    {
        if(dir == 1)
        {
            transform.position += new Vector3(0.1f,0,0);
            dir = 0;
        }else if(dir == 2) 
        {
            transform.position -= new Vector3(0.1f, 0, 0);
            dir = 0;
        }
        else if(dir == 3) 
        {
            transform.position += new Vector3(0, 0.1f, 0);
            dir = 0;
        }
        else if(dir == 4)
        {
            transform.position -= new Vector3(0, 0.1f, 0);
            dir = 0;
        }
        else if(dir == 0)
        {
            transform.position = playerPos;
            dir = 0;
        }
    }
}
