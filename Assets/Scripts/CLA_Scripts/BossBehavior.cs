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
    public GameObject wall;
    public GameObject add;

    public float maxHealth;
    [HideInInspector]
    public float realHealth;

    private SpellManager.SpellElement bossElement;
    private int addsNumber;
    private SpellManager.SpellElement addsElement;
    private int wallNumber;
    private bool addsMoving;
    private bool wallsMoving;

    public BossPattern currentBossPattern;
    public List<BossPattern> bossPatterns;

    public enum BossPhase
    {
        Phase1 = 1,
        Phase2 = 2,
        Phase3 = 3,
        Error = 0
    }
    public BossPhase currentBossPhase = BossPhase.Phase1;

    private List<GameObject> targets;
    private List<GameObject> newAdds = new List<GameObject>();
    private List<GameObject> newWalls = new List<GameObject>();

    private void Start()
    {
        GetPattern();
        LoadBossPattern();
        ApplyPattern();
    }

    private void GetPattern()
    {
        switch (currentBossPhase)
        {
            case BossPhase.Phase1:
                currentBossPattern = bossPatterns[Random.Range(0, 3)];
                break;
            case BossPhase.Phase2:
                currentBossPattern = bossPatterns[Random.Range(3, 6)];
                break;
            case BossPhase.Phase3:
                currentBossPattern = bossPatterns[Random.Range(6, 9)];
                break;
        }
    }

    private void LoadBossPattern()
    {
        bossElement = currentBossPattern.bossElement;
        addsNumber = currentBossPattern.addsNumber;
        addsElement = currentBossPattern.addsElement;
        wallNumber = currentBossPattern.wallNumber;
        addsMoving = currentBossPattern.addsMoving;
        wallsMoving = currentBossPattern.wallsMoving;
    }

    private void ApplyPattern()
    {
        //Change Boss' skin

        for (int i = 0; i < addsNumber; i++)
        {
            newAdds.Add(Instantiate(add));
            newAdds[i].GetComponent<AddBehavior>().canMove = addsMoving;
            newAdds[i].GetComponent<AddBehavior>().element = addsElement;
        }
        for (int i = 0; i < wallNumber; i++)
        {
            newWalls.Add(Instantiate(wall));
            newWalls[i].GetComponent<WallBehavior>().canMove = wallsMoving;
        }
    }

    public void BossLockTarget()
    {
        //Find a spell property on a mage and add it to the "targets" list
    }

    public void TakeDamage(float damage, SpellManager.SpellElement spellElement)
    {
        switch (spellElement)
        {
            case SpellManager.SpellElement.Fire:
                switch (bossElement)
                {
                    case SpellManager.SpellElement.Fire:
                        damage *= 1;
                        break;
                }
                break;
        }
    }
}
