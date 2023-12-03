using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSC : Singleton<PauseSC>
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject visiblePanel;
    [SerializeField] SceneSC sceneMN = new SceneSC();
    private void Start()
    {
        ShowPanel(false);
    }
    public void ToHome() 
    {
        sceneMN.LoadScene(1, true);
        ShowPanel(false); 
    }
    public void Restart()
    {
        GetCurrentScen();
        SceneManager.LoadScene(sceneName);
        ShowPanel(false);
    }
    public void ShowPanel(bool show) => visiblePanel.SetActive(show);

    private void GetCurrentScen() => sceneName = SceneManager.GetActiveScene().name;
}
