using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DataCalculator : MonoBehaviour
{

  
    public ScriptableObject data;

    //Creativity 
    public float Cp1;
    public float Cp2;
    public float Cp3;
    public float Cf;

    //Transposition 


    //Adaptability
    public float averageTurnDuration;
    public float timeRatio;
    public float timeIndicator;

    //Flexibility

    public float finalFlex;


    public void CreativityRate()
    {
        // Phase 1
        Cp1 = (GetComponent<Data>().P1numberOfDifferentSpell / GetComponent<Data>().P1numberTotalOfSpell) * 100;

        // Phase 2
        Cp2 = (GetComponent<Data>().P2numberOfSpell / GetComponent<Data>().P2numberTotalOfSpell) * 100;

        // Phase 3
        Cp3 = (GetComponent<Data>().P3numberOfSpell / GetComponent<Data>().P3numberTotalOfSpell) * 100;

        //Average
        Cf = (Cp1 + Cp2 + Cp3) / 3;

    }

    public void TranspositionRate()
    {
        averageTurnDuration = (GetComponent<Data>().playerTurnTime / GetComponent<Data>().playterTurn);

        timeRatio = (averageTurnDuration / GetComponent<Data>().maxPlayerTurnTime) * 100;

        timeIndicator = 100 - timeRatio;
    }


    public void FlexibilityRate()
    {
        finalFlex = (timeIndicator + Cf) / 3;

    }


}
