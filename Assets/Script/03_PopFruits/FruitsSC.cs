using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FruitsSC : MonoBehaviour
{
    [SerializeField] PopFruitsManager manager;
    private int countDead;

    private void Start()
    {
        manager = GameObject.Find("PopFruitManager").GetComponent<PopFruitsManager>();
        countDead = 0;
        StartCoroutine(CountToDeath());
    }
    IEnumerator CountToDeath()
    {
        yield return new WaitForSeconds(1);
        countDead++;
        if (countDead > 5)
        {
            manager.CountMiss();
            Destroy(gameObject);
        }
        StartCoroutine(CountToDeath());
    }
    private void OnMouseDown()
    {
        manager.CountScore();
        Destroy(gameObject);
    }

    private void OnMouseDrag()
    {
        manager.CountScore();
        Destroy(gameObject);
    }

    private void OnMouseUp() 
    {
        manager.CountScore();
        Destroy(gameObject);
    }
}
