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
    private Vector3 rotBody = new Vector3(0, 0, 1);

    private void Start()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = spritesEggs[0];
        manager = GameObject.Find("PopFruitManager").GetComponent<PopFruitsManager>();
        countDead = 0;
        StartCoroutine(CountToDeath());
    }
    private void Update()
    {
        SelfRot();
    }
    private IEnumerator CountToDeath()
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
    private IEnumerator CountToDisappear()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        StartCoroutine(CountToDisappear());
    }
    private void ChangeApparance() => gameObject.GetComponent<SpriteRenderer>().sprite = spritesEggs[1];
    private void SelfRot() => transform.Rotate(rotBody);


    #region Handle Touch Screen
    private void OnMouseDown()
    {
        manager.PlayPopSound();
        ChangeApparance();
        manager.CountScore();
        StartCoroutine(CountToDisappear());
    }
    #endregion
}
