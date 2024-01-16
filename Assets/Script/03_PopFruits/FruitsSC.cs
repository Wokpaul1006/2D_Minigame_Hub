using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FruitsSC : MonoBehaviour
{
    [SerializeField] PopFruitsManager manager;
    [SerializeField] List<Sprite> spritesEggs = new List<Sprite>();
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
    IEnumerator CountToDisappear()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        StartCoroutine(CountToDisappear());
    }
    private void OnMouseDown()
    {
        manager.PlayPopSound();
        ChangeApparance();
        manager.CountScore();
        StartCoroutine(CountToDisappear());
    }

    private void OnMouseDrag()
    {
        manager.PlayPopSound();
        ChangeApparance();
        manager.CountScore();
        StartCoroutine(CountToDisappear());

    }
    private void OnMouseUp()
    {
        manager.PlayPopSound();
        ChangeApparance();
        manager.CountScore();
        StartCoroutine(CountToDisappear());
    }
    private void OnMouseEnter()
    {
        manager.PlayPopSound();
        ChangeApparance();
        manager.CountScore();
        StartCoroutine(CountToDisappear());
    }
    private void ChangeApparance() => gameObject.GetComponent<Image>().sprite = spritesEggs[1];
}
