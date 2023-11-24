using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosslaneManager : MonoBehaviour
{
    [SerializeField] GameObject playerSpawner;
    [HideInInspector] SceneSC sceneMN = new SceneSC();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToHome() => sceneMN.LoadScene(1, true);
}
