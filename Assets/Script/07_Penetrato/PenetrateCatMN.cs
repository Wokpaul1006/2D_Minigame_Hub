using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenetrateCatMN : MonoBehaviour
{
    //Gameplay summary:
    //Player control an character who moving left and right contuniely by two button left and right
    //Player must choose right time to press the fire button to hit target intime
    //If player do not hit the nail intime, game will lose
    //Player must shoot bullet in the right colour, if wrong, game lose
    //Diff from level:
    //Base on RBG colour, bullet and target will have the same colour, much higher level, coliyr visible on screen harder to decide
    //Size of target decrease by level

    [SerializeField] Text levelText;
    [SerializeField] Text scoreText;
    [SerializeField] Text gameplayCountdownTime;

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
    private void UpdateScoreTxt() => scoreText.text = curScore.ToString();
    private void UpdateLeveTxt() => levelText.text = curLvl.ToString();
    #endregion
}
