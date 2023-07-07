using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSC : MonoBehaviour
{
    public void ToHome()
    {
        SceneManager.LoadScene("00_Home");
    }

    public void Restart()
    {
        SceneManager.LoadScene("01_Running");
    }
}
