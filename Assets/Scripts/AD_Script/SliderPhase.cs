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

    public float fillSpeed = 1f; 


    public void SliderTime()
    {

        SceneManager.LoadScene("AD_StatScene");

        StartCoroutine(SliderDisplay());

    }

    public IEnumerator SliderDisplay()
    {
        new WaitForSeconds(2f);

        creativity.value = Mathf.Lerp(0, GetComponent<DataCalculator>().Cf, fillSpeed);

        new WaitForSeconds(2f);

        transposition.value = Mathf.Lerp(0, GetComponent<DataCalculator>().timeIndicator, fillSpeed);

        new WaitForSeconds(2f);

        flexibility.value = Mathf.Lerp(0, GetComponent<DataCalculator>().finalFlex, fillSpeed);

        yield break; 
    }

}
