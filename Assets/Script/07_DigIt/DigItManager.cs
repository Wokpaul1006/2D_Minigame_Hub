using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigItManager : MonoBehaviour
{
    [HideInInspector] SceneSC sceneMN = new SceneSC();
    void Start()
    {
        
    }
    public void ToHome() => sceneMN.LoadScene(1, true);
}
