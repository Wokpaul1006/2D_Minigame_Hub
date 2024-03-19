using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSC : MonoBehaviour
{
    private int countToDead;

    private void Start()
    {
        StartCoroutine(Activities());
    }
    private IEnumerator Activities()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position -= new Vector3(0.1f, 0, 0);
        countToDead++;
        if(countToDead >= 60) Destroy(gameObject);
        StartCoroutine(Activities()); 
    }
}
