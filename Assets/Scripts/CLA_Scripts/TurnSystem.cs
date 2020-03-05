using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    //Init
    private static TurnSystem _instance;

    public static TurnSystem Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    //Variables
    public Text turnCountText;
    public Slider turnTimeSlider;
    public Text turnOwnerText;
    public Animator animator;

    public enum TurnOwner
    {
        Player = 1,
        Boss = 2,
        Error = 0
    }

    public TurnOwner turnOwner = TurnOwner.Player;

    public int turnCount = 1;

    public float playerTurnTime;
    public float bossTurnTime;
    [HideInInspector]
    public float realTurnTime;

    private void Update()
    {
        realTurnTime -= Time.deltaTime;
        if (realTurnTime <= 0)
        {
            NextTurn();
        }

        DisplayTurnUI();

    }

    public void NextTurn()
    {
        if(turnOwner == TurnOwner.Player)
        {
            turnOwner = TurnOwner.Boss;
            realTurnTime = bossTurnTime;

            turnOwnerText.text = "Boss Turn!";
            animator.SetTrigger("Boss' Turn");
            //Disable Player's controls
            //Cast the spell
        }
        else if(turnOwner == TurnOwner.Boss)
        {
            turnOwner = TurnOwner.Player;
            realTurnTime = playerTurnTime;
            turnCount++;

            turnOwnerText.text = "Your Turn!";
            animator.SetTrigger("Player's Turn");

            //Enable Player's controls
        }
    }

    private void DisplayTurnUI()
    {
        turnCountText.text = "Turn: " + turnCount;
        turnTimeSlider.value = realTurnTime;
        if (turnOwner == TurnOwner.Boss) turnTimeSlider.maxValue = bossTurnTime;
        else if (turnOwner == TurnOwner.Player) turnTimeSlider.maxValue = playerTurnTime;
    }
}
