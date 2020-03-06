using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class SliderPhase : MonoBehaviour
{
    public DataCalculator calculator;

    public Slider creativity;
    public Slider transposition;
    public Slider adaptability;
    public Slider flexibility;

    public float fillSpeed = 0.1f; 


    public void SliderTime()
    {

       // SceneManager.LoadScene("AD_StatScene");

        StartCoroutine(SliderDisplay());

    }

    public IEnumerator SliderDisplay()
    {
       // yield return new WaitForSeconds(2f);

        creativity.value = Mathf.Lerp(0, calculator.Cf, fillSpeed);

        yield return new WaitForSeconds(2f);

        adaptability.value = Mathf.Lerp(0, calculator.timeIndicator, fillSpeed);

        yield return new WaitForSeconds(2f);

        flexibility.value = Mathf.Lerp(0, calculator.finalFlex, fillSpeed);

        yield break; 
    }

}
