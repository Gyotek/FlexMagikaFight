using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class Data : ScriptableObject
{
    //Créativité                                        //done
    public int P1numberOfDifferentSpell = 0;
    public int P1numberTotalOfSpell = 0;

    public int P2numberOfDifferentSpell = 0;
    public int P2numberTotalOfSpell = 0;

    public int P3numberOfDifferentSpell = 0;
    public int P3numberTotalOfSpell = 0;

    public void increaseDifferentSpell()
    {
        switch (BossBehavior.Instance.currentBossPhase)
        {
            case BossBehavior.BossPhase.Phase1:
                P1numberOfDifferentSpell++;
                break;
            case BossBehavior.BossPhase.Phase2:
                P2numberOfDifferentSpell++;
                break;
            case BossBehavior.BossPhase.Phase3:
                P3numberOfDifferentSpell++;
                break;
        }
    }
    public void increaseTotalOfSpell()
    {
        switch (BossBehavior.Instance.currentBossPhase)
        {
            case BossBehavior.BossPhase.Phase1:
                P1numberTotalOfSpell++;
                break;
            case BossBehavior.BossPhase.Phase2:
                P2numberTotalOfSpell++;
                break;
            case BossBehavior.BossPhase.Phase3:
                P3numberTotalOfSpell++;
                break;
        }
    }

    //Adaptabilité
    public float playerTurnTime = 0f;
    public void addPlayerTurnTime()  => playerTurnTime += TurnSystem.Instance.timeTaken; //done

    public int playterTurn = 0;
    public void addPlayerTurn()                 => playterTurn++; //done

    public float maxPlayerTurnTime = 0f;
    public void setMaxPlayerTurnTime()          => maxPlayerTurnTime = TurnSystem.Instance.playerTurnTime; //done

    public void ResetValue() //done
    {
        P1numberOfDifferentSpell = 0;
        P1numberTotalOfSpell = 0;

        P2numberOfDifferentSpell = 0;
        P2numberTotalOfSpell = 0;

        P3numberOfDifferentSpell = 0;
        P3numberTotalOfSpell = 0;

        playerTurnTime = 0f;
        playterTurn = 0;
        maxPlayerTurnTime = 0f;
    }
}