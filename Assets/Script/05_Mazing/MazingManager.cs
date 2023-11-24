using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazingManager : MonoBehaviour
{
    //No. of Game" #04
    //Rule:
    //Player pop swipe screen to control character escape the maze.
    //Player must escape the maze before countdown time, if timeout, end game.
    //Maze shape will be more complicate than previous level.

    //Common zone
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;

    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;
    void Start()
    {
        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    public void ToHome() => sceneMN.LoadScene(1, true);
}
