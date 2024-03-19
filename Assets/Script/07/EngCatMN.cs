using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngCatMN : MonoBehaviour
{
    //Gameplay summary:
    //Player control an character who moving left and right contuniely by a button on screen
    //Player must choose right time to press the button which still below countdown time range
    //If player do not hit the nail intime, game will lose
    //Diff from level:
    //Move speed of the hammer will increase by level
    //Number of nails increase by level
    //Chance to hit the nails decrese by level

    [SerializeField] Text levelText;
    [SerializeField] Text scoreText;
    [SerializeField] Text gameplayCountdownTime;
    [SerializeField] SceneSC scene = new SceneSC();

    internal int curLvl; //This value contain current score of player in-game
    private int curScore; //This value contain current score of player in-game
    private int baseLvlValue; //This value will provide a milestone for increasing level.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Level And Scoring Handles
    internal void OnIncreasingScore()
    {
        curScore++;
        UpdateScoreTxt();
        if (curScore == baseLvlValue) OnIncreasingLevel();
    }
    internal void OnIncreasingLevel()
    {
        //This funtion will be calling at the time playerSC reach the baseLvlValue.
        //Everytime this function being called, caculating new target/new baseLvlValue.
        //When level increase, clear all enemies spawner and call Init Spawner again

        curLvl++;
        CaculatingNewTarget(curLvl);
        UpdateLeveTxt();
    }
    private void CaculatingNewTarget(int lvl) => baseLvlValue = (lvl * 10) + lvl;
    #endregion

    #region Handle UIs
    private void InitUIs()
    {
        scoreText.text = 0.ToString();
        levelText.text = 0.ToString();
    }
    public void ToHome()
    {
        scene.LoadScene(1, true);
        //Update ingame player score
    }
    private void UpdateScoreTxt() => scoreText.text = curScore.ToString();
    private void UpdateLeveTxt() => levelText.text = curLvl.ToString();
    #endregion
}
