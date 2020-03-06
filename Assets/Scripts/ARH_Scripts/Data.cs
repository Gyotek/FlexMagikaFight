using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class Data : ScriptableObject
{
    //Créativité
    public int P1numberOfDifferentSpell = 0;
    public int P1numberTotalOfSpell = 0;

    public int P2numberOfSpell = 0;
    public int P2numberTotalOfSpell = 0;

    public int P3numberOfSpell = 0;
    public int P3numberTotalOfSpell = 0;

    //Adaptabilité
    public float playerTurnTime = 0f;
    public int playterTurn = 0;
    public float maxPlayerTurnTime = 0f;
}