using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class MazingPlayerSC : MonoBehaviour
{
    [SerializeField] MazingManager m_Manager;
    public Vector3 playerPos;
    private void Start()
    {
        m_Manager = GameObject.Find("MazingManager").GetComponent<MazingManager>();
    }
    public void CharMove(int dir)
    {
        if(dir == 1)
        {

            transform.position += new Vector3(0, 0.5f, 0);
        }
        else if(dir == 2) 
        {
            transform.position -= new Vector3(0, 0.5f, 0);
        }
        else if(dir == 3) 
        {
            transform.position += new Vector3(0.5f, 0, 0);
        }
        else if(dir == 4)
        {

            transform.position -= new Vector3(0.5f, 0, 0);
        }
        else if(dir == 0)
        {
            transform.position = playerPos;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            m_Manager.LevelUp();
        }
    }
}
