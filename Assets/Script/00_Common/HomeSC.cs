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
    private float loadSpd = 0.0000001f;

    private void Start()
    {
        SetupStart();
        StartCoroutine(RunLoad());
    }
    void SetupStart()
    {
        loadSlide.value = 0;
    }

    #region Loading Zone
    IEnumerator RunLoad()
    {
        if(loadSlide.value >= 1)
        {
            ShowObject(loadPnl.gameObject, false);
            ShowObject(homePnl, true);
        }
        yield return new WaitForSeconds(0.1f);
        loadSlide.value += loadSpd * Time.deltaTime;
        StartCoroutine(RunLoad());
    }
    void ShowObject(GameObject objToShow, bool show) => objToShow.SetActive(show);
    #endregion

    #region Menu Zone

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
