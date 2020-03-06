using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene("ARH_TestScene");
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadDataScene()
    {
        SceneManager.LoadScene("AD_StatScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
