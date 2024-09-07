using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniGameManagerSC : Singleton<OmniGameManagerSC>
{
    //This class will contron the whole overall gameplay
    private sbyte platform; //-1 = nothing, 0 = PC, 1 = Android, 2 = IOS, 3 = Web base
    private void Start() 
    {
       DetectPlatform();
    }

    #region Overall Controller Function
    private void DetectPlatform()
    {

    }
    #endregion
}
