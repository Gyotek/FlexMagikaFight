using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BossPattern", menuName = "BossPattern")]
public class BossPattern : ScriptableObject
{
    public int patternID;
    public BossBehavior.BossPhase bossPhase;

    public int addsNumber;
    public int wallNumber;
    public int damage;
    public SpellManager.SpellElement bossElement;
    public SpellManager.SpellElement addsElement;

    public bool addsMoving;
    public bool wallsMoving;
}
