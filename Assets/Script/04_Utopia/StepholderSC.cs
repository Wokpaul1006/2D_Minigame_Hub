using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepholderSC : MonoBehaviour
{
    private UtopiaManager manager;
    private int currentLvl;
    private int moveDir;
    private float moveSpd;
    private int countToDead;
    void Start()
    {
        manager = GameObject.Find("UtopiaManager").GetComponent<UtopiaManager>();
        currentLvl = manager.curLevel;
        DecideMovement();
    }
    private void DecideMovement()
    {
        if(currentLvl > 10) moveSpd = currentLvl + 0.25f;
        else if (currentLvl <= 10) moveSpd = 0;
    }
    private void Update()
    {
        Movementation();
    }
    private void Movementation()
    {
        //This function allow the spawner move linear

        if (moveDir == 1 || moveDir == 0) transform.position += new Vector3(moveSpd * Time.deltaTime, 0, 0);
        else if (moveDir == -1) transform.position += new Vector3(-moveSpd * Time.deltaTime, 0, 0);

        if (transform.position.x <= -2) moveDir = 1;
        else if (transform.position.x >= 2) moveDir = -1;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") StartCoroutine(CountToDead());
    }
    IEnumerator CountToDead()
    {
        print("in count to dead step");
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
        StartCoroutine(CountToDead());
    }
}
