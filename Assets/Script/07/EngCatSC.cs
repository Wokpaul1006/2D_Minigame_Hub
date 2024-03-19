using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngCatSC : MonoBehaviour
{
    [SerializeField] GameObject hammer;
    private int moveDir;
    private bool allowMove;
    private bool allowHit;
    private Vector3 curPos;
    void Start()
    {
        allowMove = true;
        allowHit = false;
        curPos = transform.position;
    }
    void Update() 
    {
        if (allowMove == true && allowHit == false)
        {
            OnPlayerMovement();
        }else if(allowHit == true && allowMove == false) 
        {
            OnHammerMovement();
        }
    }
    #region Player Movement
    internal void OnPlayerMovement()
    {
        if (moveDir == 0)
        {
            //Case of init
            int randDir = Random.Range(0, 1);
            if (randDir == 0) 
            {
                MoveLeft();
                curPos = transform.position;
                moveDir = -1;
            } 
            else
            {
                MoveRight();
                curPos = transform.position;
                moveDir = 1;
            }
        }
        else if (moveDir == -1)
        { 
           //Case of heading left
           if(curPos.x <= -6) moveDir = 1;
            else
            {
                MoveLeft();
                curPos = transform.position;
            }
        }
        else if (moveDir == 1)
        {
            //Case of heading right
            if (curPos.x >= 6) moveDir = -1;
            else
            {
                MoveRight();
                curPos = transform.position;
            }
        }
    }
    internal void MoveLeft() => transform.position += new Vector3(-1 * Time.deltaTime, 0, 0);
    internal void MoveRight() => transform.position += new Vector3(1 * Time.deltaTime, 0, 0) ;
    #endregion

    #region Hammer movement
    private void OnHammerMovement()
    {
        hammer.transform.position += new Vector3(curPos.x, -1 * Time.deltaTime, 0);
    }
    #endregion

    public void OnHitNail()
    {
        print("drop hammer");
        allowHit = true;
        allowMove = false;
    }
}
