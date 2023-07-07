using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JumpManager : MonoBehaviour
{
    [SerializeField] JumpDinoSC dino;
    [SerializeField] JumpOstacles objs;
    [SerializeField] Transform gamezone;
    [SerializeField] GameObject pausePnl;
    [SerializeField] List<Sprite> objImage = new List<Sprite>();

    private int objImageIndex;
    // Start is called before the first frame update
    void Start()
    {
        SpawnDino();
        StartCoroutine(OnWaitToSpawnObstacle());
    }

    private void SpawnDino() => dino = Instantiate(dino, gamezone.parent);
    private IEnumerator OnWaitToSpawnObstacle()
    {
        yield return new WaitForSeconds(5);
        SpawnObstacle();
        StartCoroutine(OnWaitToSpawnObstacle());
    }
    private void SpawnObstacle()
    {
        objImageIndex = Random.Range(0, objImage.Count);
        Instantiate(objs, gamezone.parent);
        objs.SetApparance(objImage[objImageIndex]);
    }
    public void OnJump() => dino.EnableJump();
    public void ShowPause()
    {
        pausePnl = Instantiate(pausePnl);
        StopAllCoroutines();
    }
    public void ToHome()
    {
        SceneManager.LoadScene("00_Home");
    }
}
