using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class LoadSC : MonoBehaviour
{
    [SerializeField] SceneSC sceneMN = new SceneSC();
    [SerializeField] Slider loadSlide;

    [Header("Variables")]
    private float loadSpd = 0.0000001f;
    void Start()
    {
        SetupStart();
        StartCoroutine(RunLoad());
    }
    void SetupStart() 
    {

        loadSlide.value = 0;
    } 
    IEnumerator RunLoad()
    {
        if (loadSlide.value >= 1)
        {
            sceneMN.LoadScene(1, true);
        }
        yield return new WaitForSeconds(0.1f);
        loadSlide.value += loadSpd * Time.deltaTime;
        StartCoroutine(RunLoad());
    }
}
