using System.Collections;
using System.Collections.Generic;
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
  
    private void Start()
    {
        SettingStart();
        LoadUser();
    }
    private void SettingStart()
    {
        //pause = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
        listGamePanel.SetActive(true);
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
        print(listGamePanel.activeSelf);
        if (listGamePanel.activeSelf == true)
        {
            print("in unshow listgame");
            //Active/Deactive Panels
            menuPanel.SetActive(true);
            listGamePanel.SetActive(false);

            buttonAction.GetComponent<Image>().sprite = actionButtonSprite[0];
        }
        else if (menuPanel.activeSelf == true)
        {
            //Active/Deactive Panels
            menuPanel.SetActive(false);
            listGamePanel.SetActive(true);

            buttonAction.GetComponent<Image>().sprite = actionButtonSprite[1];
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
