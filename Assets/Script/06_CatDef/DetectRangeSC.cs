using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRangeSC : MonoBehaviour
{
    [SerializeField] EBullet ebullet;
    [SerializeField] MouseAttackSC enemy;
    private void Start() { }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") RepeatShooting();
    }

    private IEnumerator RepeatShooting()
    {
        yield return new WaitForSeconds(1);
        Instantiate(ebullet);
        StartCoroutine(RepeatShooting());
    }
}
