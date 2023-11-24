using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSC : MonoBehaviour
{
    [HideInInspector] PauseSC pause;
    [SerializeField] SceneSC sceneMN = new SceneSC();
    [SerializeField] List<GameObject> subPanel = new List<GameObject>();
    [SerializeField] List<Sprite> actionButtonSprite = new List<Sprite>();
    [SerializeField] GameObject listGamePanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject buttonAction;

    //Common Zone
    [SerializeField] Image playerAvt;
    [SerializeField] Text playerNameText;
    [SerializeField] Text totalScoreText;
    [SerializeField] Text highestLevelText;
    [SerializeField] Text highestScoreText;

    [HideInInspector] int butActionState;
    //0 = show List game; 1 = show Menu
    [HideInInspector] int menuSubState;
    //3 = invisible; 0 = credits; 1 = option; 2 = information
    private void Start()
    {
        SettingStart();
        LoadUser();
    }
    private void SettingStart()
    {
        pause = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
        listGamePanel.SetActive(true);
        butActionState = 0;
        menuSubState = 0; 
    }

    #region Common Section
    private void ShowPanel(int panelOder, bool show) => subPanel[panelOder].SetActive(show);
    private void LoadUser()
    {
        LoadAvatar();
        LoadHighestScore();
        LoadHighestLevel();
        LoadTotalScore();
    }
    private void LoadAvatar()
    {
        //Load PlayerPrefs for this
    }
    private void LoadTotalScore()
    {
        //Load PlayerPrefs for this
    }
    private void LoadHighestLevel()
    {
        //Load PlayerPrefs for this
    }
    private void LoadHighestScore() 
    {
        //Load PlayerPrefs for this
    }
    public void SwitchPanel() 
    {
        if(butActionState == 0)
        {
            //Case of show menu
            butActionState = 1;
            buttonAction.GetComponent<Image>().sprite = actionButtonSprite[1];

            menuPanel.SetActive(true);
            listGamePanel.SetActive(false);
        }
        else if(butActionState == 1) 
        {
            //Case of show list game
            butActionState = 0;
            buttonAction.GetComponent<Image>().sprite = actionButtonSprite[0];

            menuPanel.SetActive(false);
            listGamePanel.SetActive(true);
        }
    }
    #endregion

    #region Inside Menu Switch Panels
    public void OnShowCredits()
    {
        menuSubState = 0;
        HandlePanelVisible();
    }
    public void OnShowOption()
    {
        menuSubState = 1;
        HandlePanelVisible();
    }
    public void OnShowInfor()
    {
        menuSubState = 2;
        HandlePanelVisible();
    }
    private void HandlePanelVisible()
    {
        subPanel[menuSubState].SetActive(true);
        for(int i = 0; i< menuSubState; i++) 
        {
            if(i != menuSubState)
            {
                subPanel[i].SetActive(false);
                butActionState = 1;
            }
        }
    }
    #endregion

    #region Change Scene
    public void ToJumpDino() => sceneMN.LoadScene(2, true);
    public void ToPopFruits() => sceneMN.LoadScene(3, true);
    public void ToUtopia() => sceneMN.LoadScene(5, true);
    public void ToMazing() => sceneMN.LoadScene(4, true);
    public void ToCrosslane() => sceneMN.LoadScene(6, true);
    public void ToXXX() => sceneMN.LoadScene(5, true);
    public void ExitGame() => Application.Quit();
    #endregion
}
