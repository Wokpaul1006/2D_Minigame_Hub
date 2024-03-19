using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    [SerializeField] CatDefMN mn;
    private int countToDeath = 5;
    void Start() 
    {
        mn = GameObject.Find("DefCatManager").GetComponent<CatDefMN>();
        StartCoroutine(CountToDeath());
    } 
    void Update() => transform.position += Vector3.right * Time.deltaTime * 10;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemies")
        {
            //Add increasing score function here;
            //Do bullet hit animation here
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "EBullet")
        {
            //Do bullet hit animation here
            Destroy(gameObject);
        }
    }
    private IEnumerator CountToDeath()
    {
        //This function allow the bullet surely being destroy in case of not hit anything in runtime
        yield return new WaitForSeconds(1);
        if(countToDeath != 0) countToDeath--;
        else { Destroy(gameObject); }
        StartCoroutine(CountToDeath());
    }
}
