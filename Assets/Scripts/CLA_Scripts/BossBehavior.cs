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
    public int damage;

    public ScriptableObject currentBossPattern;

    public enum BossPhase
    {
        Phase1 = 0,
        Phase2 = 1,
        Phase3 = 2,
        Error
    }
    public BossPhase realBossPhase = BossPhase.Phase1;

    private List<GameObject> targets;

    public void BossLockTarget()
    {
        for (int i = 0; i < damage; i++)
        {
            //Find a spell property on a mage and add it to the "targets" list
        }
    }
}
