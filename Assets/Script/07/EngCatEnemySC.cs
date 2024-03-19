using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngCatEnemySC : MonoBehaviour
{
    private int moveDir;
    private Vector3 curPos;
    void Start() => curPos = transform.position;
    void Update() => OnMovement();
    #region Player Movement
    internal void OnMovement()
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
            if (curPos.x <= -6) moveDir = 1;
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
    internal void MoveRight() => transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
    #endregion
}
