using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateSC : MonoBehaviour
{
    [SerializeField] PenetrateCatMN mn = new PenetrateCatMN();
    [SerializeField] Rigidbody2D body;
    [SerializeField] SpriteRenderer render;
    private string currentColor;
    // Start is called before the first frame update
    void Start()
    {
        mn = GameObject.Find("PenetrateCatManager").GetComponent<PenetrateCatMN>();
        DecideColor();
    }

    private void DecideColor()
    {
        string charA;
        string charB;
        string charC;
        string charD;
        string charE;
        string charF;

        if(mn.curLvl > 0 && mn.curLvl <= 10)
        {
            charA = "F";
            charB = "F";
            charC = Random.Range(0, 9).ToString();
            charD = Random.Range(0, 9).ToString();
            charE = Random.Range(0, 9).ToString();
            charF = Random.Range(0, 9).ToString();
            currentColor = charA + charB + charC + charD + charE + charF;
        }

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
