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
    [SerializeField] PauseSC pausePnl;
    [SerializeField] Text currentTimeSurvive;
    [SerializeField] List<Sprite> objImage = new List<Sprite>();

    private int objImageIndex;
    private int ingameScore;
    private int timeSurvive;
    // Start is called before the first frame update
    void Start()
    {
        ingameScore = 0;
        timeSurvive = 0;

        SpawnDino();
        StartCoroutine(OnWaitToSpawnObstacle());
        StartCoroutine(CountToScore());
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
        print("in show pause");
        Instantiate(pausePnl, Vector3.zero, Quaternion.identity);
        StopAllCoroutines();
    }
    private IEnumerator CountToScore()
    {
        yield return new WaitForSeconds(1);
        timeSurvive++;
        IncreaseInGameScore();
        UpdateOnScreenSecond();
        StartCoroutine(CountToScore());
    }
    private void UpdateOnScreenSecond() => currentTimeSurvive.text = timeSurvive.ToString()+"s";
    private void IncreaseInGameScore() => ingameScore++;
    public void ToHome() => SceneManager.LoadScene("00_Home");
}
