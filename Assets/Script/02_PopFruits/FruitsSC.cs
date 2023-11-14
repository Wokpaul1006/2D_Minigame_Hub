using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FruitsSC : MonoBehaviour
{
    [SerializeField] PopFruitsManager manager;
    private int countDead;
    private bool isCountScore;

    private void Start() 
    {
        manager = GameObject.Find("PopFruitManager").GetComponent<PopFruitsManager>();
        countDead = 0;
        isCountScore = false;
        StartCoroutine(CountToDeath());
    }
    private void Update()
    {
        if(isCountScore == true)
        {
            CallToCounScore();
        }
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
    private void CallToCounScore()
    {
        //manager.CountSocre();
        manager.CounTest();
        //Destroy(gameObject);
    }
    public void OnTouchFruits() { isCountScore = true; } 
}
