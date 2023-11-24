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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        transform.position = targetPos;
        isJump = false;
    }
    private void UpdateCharPos()
    {
        //Call every time character hit new stage
        originPos = targetPos;
    }
    public void CaculatingNewTargetPos(Vector3 newPos) => targetPos = newPos;
    private void CaculatingMidPos()
    {

    }
}
