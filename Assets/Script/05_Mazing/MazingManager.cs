using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] GameObject guidePanel;
    [SerializeField] GameObject swipePanel;
    [SerializeField] MazingPlayerSC player;

    //Swiping Section
    public const float MAX_SWIPE_TIME = 0.5f; //Longer than 0.5f is not consider as swiping
    public const float MIN_SWIPE_DISTANCE = 0.17f;     // Factor of the screen width that we consider a swipe
    private Vector2 starSwipePos;
    private Vector2 currentSwipePos;
    private int swipeDir;
    //Variable swipeDir is direction of swipe. 0 is none, 1 is right, 2 is left, 3 is up 4 is down.
    private float startTime;
    //End of swiping section

    void Start()
    {
        SettingStart();
        OnShowPanel(guidePanel, true);
        OnShowPanel(swipePanel, false);
    }
    void SettingStart()
    {
        swipeDir = 0;
        starSwipePos = Vector3.zero;
        //pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    private void Update()
    {
        swipeDir = 0;
        if(Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            OnShowPanel(guidePanel, false);
            OnShowPanel(swipePanel, true);
            if(touch.phase == TouchPhase.Moved) 
            {
                Vector2 pos = touch.position;
                currentSwipePos = pos;
                CompareSwipe(currentSwipePos);
            }
        }else if(Input.touchCount == 0)
        {
            OnShowPanel(guidePanel, true);
            OnShowPanel(swipePanel, false);
        }
    }
    public void ToHome() => sceneMN.LoadScene(1, true);
    private void OnShowPanel(GameObject panel, bool show) => panel.SetActive(show);
    private void CompareSwipe(Vector2 pos)
    {
        print(pos);
        if(pos.x > 0)
        {
            player.CharMove(1);
        }else if(pos.x < 0) 
        {
            player.CharMove(2);
        }
        else if(pos.y > 0)
        {
            player.CharMove(3);
        }
        else if(pos.y < 0)
        {
            player.CharMove(4);
        }
    }
}
