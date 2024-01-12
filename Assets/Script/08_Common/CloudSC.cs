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
        yield return new WaitForSeconds(1);
        transform.position -= Vector3.right;
        countToDead++;
        if(countToDead >= 6)
        {
            Destroy(gameObject);
        }
        StartCoroutine(Activities()); 
    }
}
