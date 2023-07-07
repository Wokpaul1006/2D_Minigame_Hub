using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpOstacles : MonoBehaviour
{

    void Update()
    {
        OnMoveForward();
    }

    private void OnMoveForward() => transform.position += Vector3.left * Time.deltaTime;
    public void SetApparance(Sprite objImg)
    {
        GetComponent<Image>().sprite = objImg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Gorund") 
        {
            Destroy(gameObject);
        }
    }
}
