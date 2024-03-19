using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour
{
    private int countToDeath = 5;
    void Start() => StartCoroutine(CountToDeath());
    // Update is called once per frame
    void Update() => transform.position += Vector3.left * Time.deltaTime * 10;
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Do bullet hit animation here
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "PBullet")
        {
            //Do bullet hit animation here
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
    private IEnumerator CountToDeath()
    {
        //This function allow the bullet surely being destroy in case of not hit anything in runtime
        yield return new WaitForSeconds(1);
        if (countToDeath != 0) countToDeath--;
        else { Destroy(gameObject); }
        StartCoroutine(CountToDeath());
    }
}
