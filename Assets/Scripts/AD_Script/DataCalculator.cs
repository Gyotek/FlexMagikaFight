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
        Cp1 = (data.P1numberOfDifferentSpell / data.P1numberTotalOfSpell) * 100;

        // Phase 2
        Cp2 = (data.P2numberOfSpell / data.P2numberTotalOfSpell) * 100;

        // Phase 3
        Cp3 = (data.P3numberOfSpell / data.P3numberTotalOfSpell) * 100;

        //Average
        Cf = (Cp1 + Cp2 + Cp3) / 3;

        Debug.Log("Creativity rate = " + Cf);

    }

    public void AdaptabilityRate()
    {
        averageTurnDuration = (data.playerTurnTime / data.playterTurn);

        timeRatio = (averageTurnDuration / data.maxPlayerTurnTime) * 100;

        timeIndicator = 100 - timeRatio;

        Debug.Log("Adaptability rate = " + timeIndicator);
    }


    public void FlexibilityRate()
    {
        finalFlex = (timeIndicator + Cf) / 3;
        Debug.Log("Final Flexibility rate = " + finalFlex);
    }

    public void LaunchCalculation()
    {
        CreativityRate();
        AdaptabilityRate();
        FlexibilityRate();


       
    }

}
