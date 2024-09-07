using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsList
{
    public static string menuTip00, menuTip01, menuTip02, menuTip03, menuTip04, menuTip05, menuTip06, menuTip07, menuTip08, menuTip09, menuTip10;
    void Start() 
    {
        SetString();
    }

    private void SetString()
    {
        //Main Menu strings
        menuTip00 = "Enjoy your relaxing time!!!";
        menuTip01 = "Stop by our shop in game or website to shop new character and models";
        menuTip02 = "The Dinosaur able to release couple of bullet if it's have enough stamina";
        menuTip03 = "We have all of game type here, your job just be play them";
        menuTip04 = "This is not a Tips";
        menuTip05 = "This is Tips #5";
        menuTip06 = "this is Tips #6";
        menuTip07 = "this is Tips #7";
        menuTip08 = "this is Tips #8";
        menuTip09 = "this is Tips #9";
        menuTip10 = "this is Tips #10";
    }
}
