using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UtopiaCharSC : MonoBehaviour
{
    [HideInInspector] Rigidbody2D rb;
    [HideInInspector] Vector3 originPos; //originPos of update every time player hit new step
    [HideInInspector] Vector3 targetPos; //target pos of character, this pos equal originPos every new step
    [SerializeField] UtopiaManager manager;
    public bool isJump;
    private float jumpSpd;
    private float forwardSpd;
    private int playerDir;
    private int playerState;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumpSpd = 5f;
        forwardSpd = 0.5f;
        playerState = 0;
        SettingStart();
    }
    void SettingStart()
    {
        isJump = false;
        originPos = transform.position;
        CharDirAjuts(0);
        
    }
    void Update()
    {
        if(playerState == 1)
        {
            if (isJump)
            {
                CharJump();
            }
            CharForward();
        }
    }
    private void CharJump()
    {
        rb.AddForce(transform.up * jumpSpd, ForceMode2D.Impulse);
        isJump = false;
    }
    private void CharForward()
    {
        if(manager.CallbackGameState() == 1)
        {
            if (playerDir == 0)
            {
                //Case of move forward
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * forwardSpd);
            }
            else if (playerDir == 1)
            {
                //Case of move backward
                transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * forwardSpd);

            }
        }
    }
    public void CharDirAjuts(int dir) => playerDir = dir;
    public void CaculatingNewTargetPos(Vector3 newPos) => targetPos = newPos;
    public void DecidePlayeState(int state) => playerState = state;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Deadline")
        {
            manager.UpdateGameState(2);
        }
    }
}
