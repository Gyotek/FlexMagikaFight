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

     

    private void Start()
    {
        creativity.value = 0f;
        adaptability.value = 0f;
        flexibility.value = 0f;
        calculator.LaunchCalculation();

    }
    private void Update()
    {
        SliderAnimation();
    }
    

    public void SliderAnimation()
    {
        creativity.value = Mathf.Lerp(0f, (float)calculator.Cf, fillSpeed);
        adaptability.value = Mathf.Lerp(0f, (float)calculator.timeIndicator, fillSpeed);
        flexibility.value = Mathf.Lerp(0f, (float)calculator.finalFlex, fillSpeed);
    }
    

}
