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
                SceneManager.LoadScene("02_Jump");
                break;
            case 3:
                SceneManager.LoadScene("03_PopFruits");
                break;
            case 4:
                SceneManager.LoadScene("04_Utopia");
                break;
            case 5:
                SceneManager.LoadScene("05_Cross"); 
                break;
            case 6:
                SceneManager.LoadScene("06_Cross");
                break;
            case 7:
                SceneManager.LoadScene(" ");
                break;
            case 8:
                SceneManager.LoadScene("07_CatDef");
                break;
            case 9:
                SceneManager.LoadScene("08_Eng");
                break;
            case 10:
                SceneManager.LoadScene("09_CatPenetrator");
                break;
            case 11:
                SceneManager.LoadScene("10_CatWonder");
                break;
            case 12:
                SceneManager.LoadScene("");
                break;
            case 13:
                SceneManager.LoadScene("");
                break;
            case 14:
                SceneManager.LoadScene("");
                break;
            case 15:
                SceneManager.LoadScene("");
                break;
            case 16:
                SceneManager.LoadScene("");
                break;
            case 17:
                SceneManager.LoadScene("");
                break;
            case 18:
                SceneManager.LoadScene("");
                break;
            case 19:
                SceneManager.LoadScene("");
                break;
            case 20:
                SceneManager.LoadScene("");
                break;
            case 21:
                SceneManager.LoadScene("");
                break;
            case 22:
                SceneManager.LoadScene("");
                break;
            case 23:
                SceneManager.LoadScene("");
                break;
        }
    }
}
