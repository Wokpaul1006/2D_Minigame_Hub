using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatDefMN : MonoBehaviour
{
    //Gameplay summary:
    //Player control an character who moving up and down buy sliding the slider on screen
    //Player must shoot on mouses to prevent attack of mouses, do not let mouses reach the boundary
    //If player let mouse come across boundary, game will lose
    //Diff from level:
    //Move speed of the mouses will increase by level
    //Move speed of the mouse spawner will increase by level
    //Type of mouse spawn increase by level
    //Mouse can harm player by hitting or shooting
    //Numbers of mouse spawner increase per level stage

    [SerializeField] Slider playerControl;
    [SerializeField] CatDefSC player;
    [SerializeField] DefCatSpawner spawner;
    [SerializeField] PBullet pBullet;
    [SerializeField] SceneSC scene = new SceneSC();

    [SerializeField] Text levelText;
    [SerializeField] Text scoreText;
    [SerializeField] Text destroyedMouseText;

    internal int curLvl; //This value contain current score of player in-game
    private int curScore; //This value contain current score of player in-game
    private int baseLvlValue; //This value will provide a milestone for increasing level.
    private int numOfSpawner;
    private int destroyedMouse;

    internal int gameState;
    public bool isPaid;

    Vector3 playerPos;
    void Start()
    {
        SettingStartGame();
        InitUIs();
        InitSpawner();
    }
    private void SettingStartGame()
    {
        isPaid = true; //This var will equal PlayerPref at the releases
        gameState = 1;

        curLvl = 1;
        curScore = 0;
        baseLvlValue = 10;
        numOfSpawner = 1;
    }
    public void OnPlayerMove() => player.transform.position = new Vector3(-6.5f, playerControl.value, 0);
    public void OnPlayerShoot() => Instantiate(pBullet, player.transform.position, Quaternion.identity);
    private void DestroyAllSpawner()
    {
        for(int i = 0; i <= numOfSpawner; i++) 
        {
            Destroy(GameObject.Find("OBJ_CatDefSpawner(Clone)"));
        }
    }

    #region Level And Scoring Handles
    internal void OnIncreasingScore() 
    {
        curScore++; 
        UpdateScoreTxt();
        if (curScore == baseLvlValue) 
        {
            OnIncreasingLevel();
            scoreText.text = 0.ToString();
        }

    }
    internal void OnIncreasingLevel()
    {
        //This funtion will be calling at the time playerSC reach the baseLvlValue.
        //Everytime this function being called, caculating new target/new baseLvlValue.
        //When level increase, clear all enemies spawner and call Init Spawner again

        curLvl++;
        CaculatingNewTarget(curLvl);
        UpdateLeveTxt();
        DestroyAllSpawner();
        InitSpawner();
    }
    private void CaculatingNewTarget(int lvl) => baseLvlValue = (lvl * 10) + lvl;
    #endregion

    #region Generate Gameplay
    private void InitSpawner()
    {
        if (curLvl > 0 && curLvl <= 10)
        {
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
        }
        else if (curLvl > 10 && curLvl <= 20)
        {
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            numOfSpawner = 2;
        }
        else if (curLvl > 20 && curLvl <= 40)
        {
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            numOfSpawner = 3;
        }
        else if (curLvl > 40 && curLvl <= 80)
        {
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            numOfSpawner = 4;
        }
        else if (curLvl > 80 && curLvl <= 120)
        {
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            numOfSpawner = 5;
        }
        else if (curLvl > 120 && curLvl <= 160)
        {
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            Instantiate(spawner, new Vector3(10, 0, 0), Quaternion.identity);
            numOfSpawner = 6;
        }
    }
    #endregion

    #region Handle UIs
    private void InitUIs()
    {
        scoreText.text = 0.ToString();
        levelText.text = curLvl.ToString();
        destroyedMouseText.text = 0.ToString();
        playerControl.value = 0;
    }
    public void ToHome() 
    {
        scene.LoadScene(1, true);
        //Update ingame player score
    }
    public void OnFire()
    {
        OnPlayerShoot();
    }
    private void UpdateScoreTxt() => scoreText.text = curScore.ToString();
    private void UpdateLeveTxt() => levelText.text = curLvl.ToString();
    public void UpdateMouseDestroy()
    {
        destroyedMouseText.text = (destroyedMouse++).ToString();
    }
    #endregion
}
