using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class CreditPanelSC : MonoBehaviour
{
    [SerializeField] Text titleTxt;
    [SerializeField] Text descriptionTxt01;
    [SerializeField] Text descriptionTxt02;
    [SerializeField] Text descriptionTxt03;
    [SerializeField] Text descriptionTxt04;
    [SerializeField] Text descriptionTxt05;
    [SerializeField] Text descriptionTxt06;

    private string urlYTB = "https://www.youtube.com/channel/UCIzq6IKOnDm6a1_azxVYlFQ";
    private string urlTwitter = "https://twitter.com/SadekGame15769";
    private string urlFacebook = "https://www.facebook.com/people/Sadek-Game-Studio/100083454556637/";
    private string urlWebsite = "https://sadekgame.wordpress.com/";
    private string urlIG = "";
    private string urlTiktok = "https://www.tiktok.com/@sadekgamestudio23";

    #region Credits Handlers
    public void OnShowGameCredits()
    {
        titleTxt.text = "GAME CREDITS";
        descriptionTxt01.text = "Publisher: Sadek Games Studio";
        descriptionTxt02.text = "Artist & Graphics: Nautilus";
        descriptionTxt03.text = "Developer: BaoBao";
        descriptionTxt04.text = "Game Designer: Wok Paul";
        descriptionTxt05.text = "Sound & SFX: Varius Youtube Artist";
        descriptionTxt06.text = "Project Manager: Sadek Games Studio";
    }
    public void OnShowStudioCredits()
    {
        titleTxt.text = "STUDIO CREDITS";
        descriptionTxt01.text = "We are Sadek Game Studio";
        descriptionTxt02.text = "We create mobile apps";
        descriptionTxt03.text = "We make relaxing games";
        descriptionTxt04.text = "Join us to conquer our universe";
        descriptionTxt05.text = "";
        descriptionTxt06.text = "";
    }
    #endregion

    #region Social Connect Handlers
    public void ToIG() => Application.OpenURL(urlIG);
    public void ToFB() => Application.OpenURL(urlFacebook);
    public void ToTwitter() => Application.OpenURL(urlTwitter);
    public void ToTikTok() => Application.OpenURL(urlTiktok);   
    public void ToWebsite() => Application.OpenURL(urlWebsite);
    public void ToYTB() => Application.OpenURL(urlYTB);
    #endregion
}
