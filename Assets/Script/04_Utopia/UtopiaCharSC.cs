using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UtopiaCharSC : MonoBehaviour
{
    [HideInInspector] Rigidbody2D rb;
    [HideInInspector] Vector3 originPos; //originPos of update every time player hit new step
    [HideInInspector] Vector3 middlePos; //position of middle jump period
    [HideInInspector] Vector3 targetPos; //target pos of character, this pos equal originPos every new step
    public bool isJump;
    private bool allowFor
    private float jumpSpd;
    private float moveSpd;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumpSpd = 5f;
        moveSpd = 2f;
        SettingStart();
    }
    void SettingStart()
    {
        isJump = false;
        originPos = transform.position;
    }
    void Update()
    {
        if(isJump) 
        {
            CharJump();
        }
    }
    private void CharJump()
    {
        //transform.position = targetPos;
        print(transform.forward);
        rb.AddForce(transform.up * jumpSpd, ForceMode2D.Impulse);
        //rb.AddForce(transform.forward * moveSpd, ForceMode2D.Force);
        isJump = false;
    }
    private void UpdateCharPos()
    {
        //Call every time character hit new stage
        originPos = targetPos;
    }
    public void CaculatingNewTargetPos(Vector3 newPos) => targetPos = newPos;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision == )
    }
}
