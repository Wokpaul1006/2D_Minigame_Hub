using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigItManager : MonoBehaviour
{
    //No. of Game" #06
    //Rule:
    //Player pop the fruit fall from the spawner, if miss more tha 5 fruist, game end.

    //Common zone
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;

    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;

    //Specific zone

    void Start()
    {

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    public void ToHome() => sceneMN.LoadScene(1, true);
}
