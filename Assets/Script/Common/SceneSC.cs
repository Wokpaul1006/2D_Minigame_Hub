using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSC
{
    [SerializeField] List<Scene> sceneList;

    public void LoadScene(int sceneOder, bool isLoad)
    {
        switch (sceneOder)
        {
            case 0:
                SceneManager.LoadScene("00_LoadScene");
                break;
            case 1:
                SceneManager.LoadScene("01_Home");
                break;
            case 2:
                Debug.Log("in call jump dino scene");
                SceneManager.LoadScene("02_Jump");
                break;
            case 3:
                SceneManager.LoadScene("03_Pop");
                break;
            case 4:
                SceneManager.LoadScene("04_Mazing");
                break;
            case 5:
                SceneManager.LoadScene("05_Utopia");
                break;
            case 6:
                SceneManager.LoadScene("06_Cross");
                break;
            case 7:
                SceneManager.LoadScene("07_DigIt");
                break;
        }
    }
}
