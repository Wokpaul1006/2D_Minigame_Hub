using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitsSC : MonoBehaviour
{
    public void DestroyOnClick()
    {
        gameObject.GetComponent<Image>().enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
