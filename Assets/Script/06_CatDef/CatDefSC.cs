using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CatDefSC : MonoBehaviour
{

    [SerializeField] Image hpBar;
    private CatDefMN mn;
    private Rigidbody2D body;
    private Vector3 touchPos;

    private float moveSpd;
    private float atkSpd;
    private float atkDmg;
    private float detectRange;
    private float playerHP;

    //private bool isTakeDamage;

    void Start()
    {
        OnStartGame();
        SettingPlayerStat();
    }

    private void OnStartGame()
    {
        mn = GameObject.Find("DefCatManager").GetComponent<CatDefMN>();
        body = GetComponent<Rigidbody2D>();

        body.transform.position = new Vector3(-6.5f, 0, 0); //Setup first pos of 
    }
    private void SettingPlayerStat()
    {
        //those code under this line is development-only, once the game fully roll out, those stat equal PlayerPrefs stat except of comment ones
        //isTakeDamage = false; //This variable will not being override by PlayerPrefs
        playerHP = 1;

        hpBar.fillAmount = playerHP;
    }
    internal void DecreasingPlayerHP()
    {
        hpBar.fillAmount -= 0.1f;
        playerHP = hpBar.fillAmount;
        //isTakeDamage = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EBullet" || collision.gameObject.tag == "Enemies")
        {
            DecreasingPlayerHP();
            if (hpBar.fillAmount <= 0) mn.ToHome();
        }
    }
    //private IEnumerator CountToTakeDamage(float curHP)
    //{
    //    //This function will decide if player is taking damage or not.
    //    //Counting for 3 secound, if in 3 second player not take damage, player will healing overtime
    //    yield return new WaitForSeconds(3);
    //    if(curHP == playerHP)
    //    {
    //        isTakeDamage = false;
    //        HealPlayerHealth();
    //    }
    //    StartCoroutine(CountToTakeDamage(curHP));
    //}

    private void HealPlayerHealth()
    {
        playerHP = 1;
        hpBar.fillAmount = playerHP;
    }
}
