using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSC : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject homePnl;
    [SerializeField] GameObject loadPnl;
    [SerializeField] GameObject usernamePnl;
    [SerializeField] Slider loadSlide;

    [Header("Variables")]
    private float loadSpd;

    private void Start()
    {
        SetupStart();
        RunLoad();
    }
    private void Update()
    {
        
    }

    #region Common Zone
    void SetupStart()
    {
        loadSlide.value = 0;
    }
    void RunLoad()
    {
        if(loadSlide.value >= 1)
        {
            ShowObject(loadPnl.gameObject, false);
            ShowObject(homePnl, true);
        }
        loadSlide.value += loadSpd * Time.deltaTime;
    }
    void ShowObject(GameObject objToShow, bool show) => objToShow.SetActive(show);
    #endregion

    #region Change panel
    public void ExitGame() => Application.Quit();
    #endregion

    #region Change Scene
    public void ToRunningGame() => SceneManager.LoadScene("01_Running");
    public void ToCutFruits() => SceneManager.LoadScene("02_PopsFruits");
    public void ToTheMoon() => SceneManager.LoadScene("03_Mazing");
    public void ToMazing() => SceneManager.LoadScene("04_Utopia");
    #endregion
}
