using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosslaneManager : MonoBehaviour
{
    //No. of Game" #05
    //Rule:
    //Player press the 

    //Common zone
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;

    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;

    //Specific zone
    [SerializeField] GameObject playerSpawner;

    // Start is called before the first frame update
    void Start()
    {

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToHome() => sceneMN.LoadScene(1, true);
}
