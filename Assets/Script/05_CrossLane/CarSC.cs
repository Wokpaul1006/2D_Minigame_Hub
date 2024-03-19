using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSC : MonoBehaviour
{
    private Vector2 originPos;
    private int countToDead;
    private float moveSpd; //this variable base on curent level, the hiher level, car move faster by 0.1f;
    void Start()
    {
        SettingStart();
        StartCoroutine(CountToDestroyCar());
    }

    void Update() => LinearMove();
    private void SettingStart()
    {
        //Containt all start-period properties of objects
        countToDead = 5;
        moveSpd = 1f;
        originPos = transform.position;
    }

    private IEnumerator CountToDestroyCar()
    {
        yield return new WaitForSeconds(1);
        countToDead--;
        if(countToDead <= 0 )
            Destroy(gameObject);
        StartCoroutine(CountToDestroyCar());
    }
    private void LinearMove()
    {
        if (originPos.x < 0)
        {
            transform.position += Vector3.right * moveSpd * Time.deltaTime;
        }
        else if(originPos.x >= 0)
        {
            transform.position += Vector3.left * moveSpd * Time.deltaTime;
        }
    }
    public void UpdateSpeed(int level) => moveSpd = Random.Range(0.5f, 2f);
}
