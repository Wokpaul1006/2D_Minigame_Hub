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
    private void Update()
    {

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
    private void OnMouseEnter()
    {
        Destroy(gameObject);
        manager.CountSocre();
    }
}
