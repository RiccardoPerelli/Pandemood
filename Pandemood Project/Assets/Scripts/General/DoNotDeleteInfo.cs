using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDeleteInfo : MonoBehaviour
{
    private static int _levelNo; //number of level you completed
    private static bool _character; //false is male, true is girl
    // Start is called before the first frame update
       
    public bool getCharacter()
    {
        return _character;
    }
    public void setCharacter(bool yourChoice)
    {
        _character = yourChoice;
    }
    public int getLevelNo()
    {
        return _levelNo;
    }
    public void setLevelNo(int newLevel)
    {
        _levelNo = newLevel;
    }
}
