using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WonderCatMN : MonoBehaviour
{
    //Gameplay summary:
    //Player control an character who fly fowarly but being effected by gravity
    //Player must press on screen to keep character still fly on air
    //Obstacle will spawn along the way
    //If player let character hit the floor or obstacle, game will lose
    //Diff from level:
    //Move speed of the character will increase by level
    //Gravity of the character will increase by level
    //Numbers of obstacle will spawn more due to level

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
