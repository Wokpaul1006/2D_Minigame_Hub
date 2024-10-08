using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSC : MonoBehaviour
{
    [SerializeField] SceneSC sceneMN = new SceneSC();
    [SerializeField] Slider loadSlide;

    [SerializeField] Text loadTips;

    [Header("Variables")]
    private float loadSpd;
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
        loadSpd = Random.Range(0.01f, 0.9f);
        if (loadSlide.value >= 1)
        {
            sceneMN.LoadScene(1, true);
        }
        yield return new WaitForSeconds(0.1f);
        loadSlide.value += loadSpd * Time.deltaTime * 10;
        UpdateTips(loadSlide.value);
        StartCoroutine(RunLoad());
    }

    private void UpdateTips(float value)
    {
        if (value <= 0.25f) loadTips.text = "Loading...";
        else if (value <= 0.5f && value > 0.25f) loadTips.text = "Reading player info...";
        else if (value > 0.5f && value < 0.75f) loadTips.text = "Seat tight...";
        else if (value >= 0.75f && value <= 1) loadTips.text = "Entering Game";
    }
}
