using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    //Init
    private static BossBehavior _instance;

    public static BossBehavior Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    //Variables
    public float maxHealth;
    [HideInInspector]
    public float realHealth;

    private int damage;
    private SpellManager.SpellElement bossElement;
    private int addsNumber;
    private SpellManager.SpellElement addsElement;
    private int wallNumber;
    private bool addsMoving;
    private bool wallsMoving;

    public BossPattern currentBossPattern;

    public enum BossPhase
    {
        Phase1 = 1,
        Phase2 = 2,
        Phase3 = 3,
        Error = 0
    }
    public BossPhase currentBossPhase = BossPhase.Phase1;

    private List<GameObject> targets;

    private void LoadBossPattern()
    {
        damage = currentBossPattern.damage;
        bossElement = currentBossPattern.bossElement;
        addsNumber = currentBossPattern.addsNumber;
        addsElement = currentBossPattern.addsElement;
        wallNumber = currentBossPattern.wallNumber;
        addsMoving = currentBossPattern.addsMoving;
        wallsMoving = currentBossPattern.wallsMoving;
    }


    public void BossLockTarget()
    {
        for (int i = 0; i < damage; i++)
        {
            //Find a spell property on a mage and add it to the "targets" list
        }
    }
}
