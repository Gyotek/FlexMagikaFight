using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DataCalculator : MonoBehaviour
{

  
    public Data data;

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
        Cp1 = ((float)data.P1numberOfDifferentSpell / (float)data.P1numberTotalOfSpell) * 100f;

        Debug.Log("DifferentSpell = " + data.P1numberOfDifferentSpell);
        Debug.Log("TotalSpell = " + data.P1numberTotalOfSpell);
        Debug.Log("Cp1 = " + Cp1);

        // Phase 2
        Cp2 = ((float)data.P2numberOfDifferentSpell / (float)data.P2numberTotalOfSpell) * 100f;
        Debug.Log("Cp2 = " + Cp2);
        // Phase 3
        Cp3 = ((float)data.P3numberOfDifferentSpell / (float)data.P3numberTotalOfSpell) * 100f;
        Debug.Log("Cp3 = " + Cp3);
        //Average
        Cf = (Cp1 + Cp2 + Cp3) / 3;

        Debug.Log("Creativity rate = " + Cf);

    }

    public void AdaptabilityRate()
    {
        averageTurnDuration = ((float)data.playerTurnTime / (float)data.playterTurn);

        timeRatio = ((float)averageTurnDuration / (float)data.maxPlayerTurnTime) * 100f;

        timeIndicator = (100 - timeRatio) + 10;

        Debug.Log("Adaptability rate = " + timeIndicator);
    }


    public void FlexibilityRate()
    {
        finalFlex = (timeIndicator + Cf) / 2;
        Debug.Log("Final Flexibility rate = " + finalFlex);
    }

    public void LaunchCalculation()
    {
        CreativityRate();
        AdaptabilityRate();
        FlexibilityRate(); 
    }

}
