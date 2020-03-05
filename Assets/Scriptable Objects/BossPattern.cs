using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BossPattern", menuName = "BossPattern")]
public class BossPattern : ScriptableObject
{
    public int patternID;
    public int bossphase;

    public int addsNumber;
    public int wallNumber;
    public int damage;
    public int bossElement; //0 = Fire, 1 = Water, 2 = Plant
    public int addsElement; //0 = Fire, 1 = Water, 2 = Plant

    public bool addsMoving;
    public bool wallsMoving;
}
