using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseAttackSC : MonoBehaviour
{
    [SerializeField] CatDefMN mn;
    [SerializeField] EBullet eBullet;
    [SerializeField] DetectRangeSC detect;
    private int mouseType;
    private int mouseHealth;
    private string tempName = "(Clone)";
    void Start()
    {
        mn = GameObject.Find("DefCatManager").GetComponent<CatDefMN>();
        DecideBehaviour();
    }
    void Update()
    {
        if (mouseType == 1 || mouseType == 3 || mouseType == 4) MoveLinear();
        else if (mouseType == 2) MoveToPlayer();
    }

    void DecideBehaviour()
    {
        if(gameObject.name == "OBJ_Mouses_Cub"+ tempName)
        {
            //Move Linear
            mouseType = 1;
            mouseHealth = 1;
            detect = null;
        }
        else if (gameObject.name == "OBJ_Mouses_Medium"+ tempName)
        {
            //Move Linear
            mouseType = 1;
            mouseHealth = 2;
            detect = null;
        }
        else if (gameObject.name == "OBJ_Mouses_Big" + tempName)
        {
            //Move Linear
            mouseType = 1;
            mouseHealth = 3;
            detect = null;
        }
        else if (gameObject.name == "OBJ_Mouses_Chase" + tempName)
        {
            //Move to player
            mouseType = 2;
            mouseHealth = 2;
            detect = null;
        }
        else if (gameObject.name == "OBJ_Mouses_RangeSingle" + tempName)
        {
            //Move Linear and fire sigle bullet to player
            mouseType = 3;
            mouseHealth = 2;
            detect = new DetectRangeSC();
            StartCoroutine(ShotBullet(mouseType));
        }
        else if (gameObject.name == "OBJ_Mouses_RangeBrust" + tempName)
        {
            //Move Linear and fire brust bullet to player
            mouseType = 4;
            mouseHealth = 2;
            detect = new DetectRangeSC();
            StartCoroutine(ShotBullet(mouseType));
        }
    }
    private void MoveLinear()
    {
        transform.position += Vector3.left * Time.deltaTime;
    }
    private void MoveToPlayer()
    {
        Vector3 playerPos = GameObject.Find("OBJ_Player").GetComponent<Transform>().position;
        gameObject.transform.position = playerPos; 
    }
    private IEnumerator ShotBullet(int mouseType)
    {
        yield return new WaitForSeconds(2);
        if (mouseType == 3) Instantiate(eBullet, transform.position, Quaternion.identity);
        else if (mouseType == 4)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(eBullet, transform.position, Quaternion.identity);
            }
        }
        StartCoroutine(ShotBullet(mouseType));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PBullet")
        {
            mouseHealth--;
            if(mouseHealth <= 0)
            {
                mn.OnIncreasingScore();
                mn.UpdateMouseDestroy();
                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.tag == "Player")
        {
            //Decreasing player's health
            //If player health = 0, game loose
            Destroy(gameObject);
        }else if(collision.gameObject.tag == "DefPoint")
        {
            //Game loose
            mn.ToHome();
        }
    }
}
