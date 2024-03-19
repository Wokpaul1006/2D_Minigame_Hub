using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosslaneCharSC : MonoBehaviour
{
    private int playerDir;
    private float moveSpd = 2f;
    private CrosslaneManager mn;

    void Start()
    {
        playerDir = 0;
        mn = GameObject.Find("CrossLaneManager").GetComponent<CrosslaneManager>();
    }
    private void OnMoving(int direct)
    {
        if(direct != 0)
        {
            if (direct == 1)
            {
                transform.position += Vector3.left * moveSpd * 0.5f;
                playerDir = 0;
            }       
            else if (direct == 2)
            {
                transform.position += Vector3.right * moveSpd * 0.5f;
                playerDir = 0;
            }    
            else if (direct == 3)
            {
                transform.position += Vector3.up * moveSpd * 0.5f;
                playerDir = 0;
            }
        }
    }
    public void SetDir(int dir) 
    {
        playerDir = dir;
        OnMoving(playerDir);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            mn.LevelUp();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            mn.ShowPause();
        }
    }
}
