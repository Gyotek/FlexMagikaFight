using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    //Init
    private static BossBehavior _instance;

    public static BossBehavior Instance { get { return _instance; } }

    private SpriteRenderer sprRenderer;
    private RectTransform healthbar;

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

        sprRenderer = GetComponent<SpriteRenderer>();
        healthbar = GameObject.Find("HealthBar").GetComponent<RectTransform>();
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
    public GameEvent onNewPhase;

    private List<GameObject> targets;
    private List<GameObject> newAdds = new List<GameObject>();
    private List<GameObject> newWalls = new List<GameObject>();

    private void Start()
    {
        realHealth = maxHealth;
        GetPattern();
    }

    private void Update()
    {
        healthbar.localScale = new Vector3(realHealth/maxHealth, 1f, 1f);

        if(realHealth <= 0)
        {
            Debug.Log("Boss is defeated!");
        }
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
        LoadBossPattern();
    }

    private void LoadBossPattern()
    {
        bossElement = currentBossPattern.bossElement;
        addsNumber = currentBossPattern.addsNumber;
        addsElement = currentBossPattern.addsElement;
        wallNumber = currentBossPattern.wallNumber;
        addsMoving = currentBossPattern.addsMoving;
        wallsMoving = currentBossPattern.wallsMoving;

        ApplyPattern();
    }

    public void ChangeBossPhase()
    {
        switch (currentBossPhase)
        {
            case BossPhase.Phase1:
                if(realHealth <= (maxHealth/3*2))
                {
                    Debug.Log("Phase 2!");
                    currentBossPhase = BossPhase.Phase2;
                    onNewPhase.Raise();
                    GetPattern();
                }
                break;
            case BossPhase.Phase2:
                if(realHealth <= (maxHealth/3))
                {
                    Debug.Log("Phase 3!");
                    currentBossPhase = BossPhase.Phase3;
                    onNewPhase.Raise();
                    GetPattern();
                }
                break;
        }
    }

    private void ApplyPattern()
    {
        switch (bossElement)
        {
            case SpellManager.SpellElement.Fire:
                sprRenderer.color = Color.red;
                break;
            case SpellManager.SpellElement.Plant:
                sprRenderer.color = Color.green;
                break;
            case SpellManager.SpellElement.Water:
                sprRenderer.color = Color.blue;
                break;
            case SpellManager.SpellElement.ERROR:
                sprRenderer.color = Color.grey;
                break;
        }

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
        newAdds.Clear();
        newWalls.Clear();
    }

    public void BreakSpell()
    {
        var element = Random.Range(0, 9);
        switch(element)
        {
            case 0:
                var fire = SpellManager.instance.ButtonFinder(SpellManager.SpellElement.Fire);
                fire.enabled = false;
                break;
            case 1:
                var water = SpellManager.instance.ButtonFinder(SpellManager.SpellElement.Water);
                water.enabled = false;
                break;
            case 2:
                var plant = SpellManager.instance.ButtonFinder(SpellManager.SpellElement.Plant);
                plant.enabled = false;
                break;
            case 3:
                var bounce = SpellManager.instance.ButtonFinder(SpellManager.SpellType.Bounce);
                bounce.enabled = false;
                break;
            case 4:
                var impact = SpellManager.instance.ButtonFinder(SpellManager.SpellType.Impact);
                impact.enabled = false;
                break;
            case 5:
                var perfo = SpellManager.instance.ButtonFinder(SpellManager.SpellType.Perforant);
                perfo.enabled = false;
                break;
            case 6:
                var circle = SpellManager.instance.ButtonFinder(SpellManager.SpellZone.Circle);
                circle.enabled = false;
                break;
            case 7:
                var line = SpellManager.instance.ButtonFinder(SpellManager.SpellZone.Line);
                line.enabled = false;
                break;
            case 8:
                var multi = SpellManager.instance.ButtonFinder(SpellManager.SpellZone.Multiple);
                multi.enabled = false;
                //multi.
                break;
        }
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
                    case SpellManager.SpellElement.Plant:
                        damage *= 2;
                        break;
                    case SpellManager.SpellElement.Water:
                        damage *= 0;
                        break;
                }
                break;
            case SpellManager.SpellElement.Plant:
                switch (bossElement)
                {
                    case SpellManager.SpellElement.Fire:
                        damage *= 0;
                        break;
                    case SpellManager.SpellElement.Plant:
                        damage *= 1;
                        break;
                    case SpellManager.SpellElement.Water:
                        damage *= 2;
                        break;
                }
                break;
            case SpellManager.SpellElement.Water:
                switch (bossElement)
                {
                    case SpellManager.SpellElement.Fire:
                        damage *= 2;
                        break;
                    case SpellManager.SpellElement.Plant:
                        damage *= 0;
                        break;
                    case SpellManager.SpellElement.Water:
                        damage *= 1;
                        break;
                }
                break;
        }
        realHealth -= damage;
    }
}
