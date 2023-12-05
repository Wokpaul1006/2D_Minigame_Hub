using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazingManager : MonoBehaviour
{
    //No. of Game" #03
    //Rule:
    //Player pop swipe screen to control character escape the maze.
    //Player must escape the maze before countdown time, if timeout, end game.
    //Maze shape will be more complicate than previous level.

    //Common zone
    [SerializeField] Text curretScore;
    [SerializeField] Text currentLevel;

    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;

    //Specific Zone
    [SerializeField] MazingPlayerSC player;
    [SerializeField] Text countDownTimeToEsscape;
    [SerializeField] GameObject ground;
    [SerializeField] GameObject border;
    [SerializeField] GameObject goal;
    [HideInInspector] List<GameObject> mazeElements = new List<GameObject>();

    private int charDir;
    private int countdownVar; //This var is re-caculating everytime the game increazse level.
    //Logic of "coundownVar is base on curent level
    private int curLvl;
    private int baseLvl = 1;

    void Start()
    {
        SettingStart();
        DrawPlayGround();
    }
    void SettingStart()
    {
        charDir = 0;
        countdownVar = 0;
        curLvl = baseLvl;
        currentLevel.text = baseLvl.ToString();
        //pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    private  void DrawPlayGround()
    {
        //Call this at start game and everytime update level
        if(curLvl > 1)
            ClearMaze();
        InitCage(curLvl);
        //InitMaze(curLvl);
        InitSpawnPoint();
        InitGoal();
    }
    public void ToHome() => sceneMN.LoadScene(1, true);
    private void OnShowPanel(GameObject panel, bool show) => panel.SetActive(show);

    #region Handle Movement Input Events
    public void OnClickUp()
    {
        charDir = 1;
        player.CharMove(charDir);
        charDir = 0;
    }
    public void OnClickDown()
    {
        charDir = 2;
        player.CharMove(charDir);
        charDir = 0;
    }
    public void OnClickRight()
    {
        charDir = 3;
        player.CharMove(charDir);
        charDir = 0;
    }
    public void OnClickLeft()
    {
        charDir = 4;
        player.CharMove(charDir);
        charDir = 0;
    }
    #endregion

    #region Handle Draw Level
    private void InitCage(int curLevel)
    {
        Vector3 startBorderPos = new Vector3(-2, 2, 0);
        ground = Instantiate(ground, Vector3.zero, Quaternion.identity);
        if (curLvl <= 10)
        {
            for (int i = 0; i < 8; i++)
            {
                //Draw upper border
                if (i == 0) Instantiate(border, new Vector3(-2,2,0), Quaternion.identity);
                else if(i ==7) Instantiate(border, new Vector3(-2, 2, 0), Quaternion.identity);
                else if (i == 3) Instantiate(border, new Vector3(0, 2, 0), Quaternion.identity);
                else Instantiate(border, new Vector3(startBorderPos.x + i, 2, 0), Quaternion.identity);
            }
        }
    }
    private void InitSpawnPoint()
    {
        int randSpawnPointX, randSpawnPointY;
        randSpawnPointX = randSpawnPointY = Random.Range(-2, 0);
        player.transform.position = new Vector3(randSpawnPointX, randSpawnPointY, 0);
    }
    private void InitGoal()
    {
        float randSpawnPointX, randSpawnPointY;
        randSpawnPointX = randSpawnPointY = Random.Range(-3, 3);
        goal = Instantiate(goal,new Vector3(randSpawnPointX, randSpawnPointY, 0), Quaternion.identity);
    }
    private void InitMaze(int curLevel)
    {
        if (curLevel <= 5)
        {
            for(int i = 0; i <= 5; i++)
            {
                float randSpawnPointX, randSpawnPointY;
                randSpawnPointX = randSpawnPointY = Random.Range(-3, 3);
                Instantiate(border, new Vector3(randSpawnPointX, randSpawnPointY, 0), Quaternion.identity);
                mazeElements.Add(border);
            }
        }
        else if (curLevel <= 10)
        {

        }
        else if (curLevel <= 20)
        {

        }
        else if (curLevel <= 50)
        {

        }
        else if (curLevel >= 100)
        {

        }
    }
    private void ClearMaze()
    {
        Destroy(goal);
        mazeElements.Clear();
    }
    #endregion

    #region Handle Gameplay
    public void LevelUp()
    {
        curLvl++;
        UpdateGameplay();
    }
    private void UpdateGameplay()
    {
        DrawPlayGround();
        currentLevel.text = curLvl.ToString();
    }
    #endregion
}
