using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InforPanel : MonoBehaviour
{
    [SerializeField] GameObject desPanel, infoPanel;
    [SerializeField] List<Sprite> gameIconsList = new List<Sprite>();
    
    [SerializeField] Text panelNameTxt;
    [SerializeField] Text panelDesTxt;
    [SerializeField] Image gameIcon;

    private string panelName;
    private string panelDes;
    private int iconOder;

    private void Start()
    {
        panelName = "";
        panelDes = "";
        iconOder = 0;
        
        panelNameTxt.text = panelName;
        panelDesTxt.text = panelDes;
        gameIcon.GetComponent<Image>().sprite = gameIconsList[iconOder];
    }

    #region Handle Data Call
    public void OnClickDinoJump()
    {
        panelName = "Jump Jump Dino";
        panelDes = "Press the button to make the Dino jump, avoid those Obstacle!!";
        iconOder = 1;
        UpdatePanelContents(panelName, iconOder, panelDes);
    }
    public void OnClickPopFruits()
    {
        panelName = "Pop Pop Fruits";
        panelDes = "Touch the fruits to collect them, do not miss more than 10 fruits";
        iconOder = 2;
        UpdatePanelContents(panelName, iconOder, panelDes);
    }
    public void OnClickMazingOut()
    {
        panelName = "Mazing Out";
        panelDes = "Press those navigator button to escape the maze before time running out!!";
        iconOder = 3;
        UpdatePanelContents(panelName, iconOder, panelDes);
    }
    public void OnClickUtopia()
    {
        panelName = "Utopia Chaser";
        panelDes = "The time hase come, Noah has been son his work, Noah Ship is here, avoid the flood!!";
        iconOder = 4;
        UpdatePanelContents(panelName, iconOder, panelDes);
    }
    public void OnClickCrossLane()
    {
        panelName = "Cross Lane";
        panelDes = "Try to make you way through the street full of cars and truck!";
        iconOder = 5;
        UpdatePanelContents(panelName, iconOder, panelDes);
    }
    public void OnClickDigIt()
    {
        panelName = "Jump Jump Dino";
        panelDes = "Press the button to make the Dino jump, avoid those Obstacle!!";
        iconOder = 6;
        UpdatePanelContents(panelName, iconOder, panelDes);
    }
    #endregion

    #region Handle UIs
    void UpdatePanelContents(string name, int imgOder, string des)
    {
        panelNameTxt.text = name;
        panelDesTxt.text = des;
        gameIcon.GetComponent<Image>().sprite = gameIconsList[imgOder];

        OnShowPanels(true);
    }
    void OnShowPanels(bool show)
    {
        infoPanel.SetActive(!show);
        desPanel.SetActive(show);
    }
    public void ClearDes()
    {
        OnShowPanels(false);
        panelName = "";
        panelDes = "";
        iconOder = 0;

        panelNameTxt.text = panelName;
        panelDesTxt.text = panelDes;
        gameIcon.GetComponent<Image>().sprite = gameIconsList[iconOder];
    }
    #endregion
}
