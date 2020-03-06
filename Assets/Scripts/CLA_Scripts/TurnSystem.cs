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

    public float timeTaken;

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

    [SerializeField]
    private GameEvent onNextTurn;
    [SerializeField]
    private GameEvent onBossTurnStart;
    [SerializeField]
    private GameEvent onPlayerTurnStart;

    private void Update()
    {
        realTurnTime -= Time.deltaTime;

        if (turnOwner == TurnOwner.Player)
        {
            timeTaken += Time.deltaTime;
        }

        if (realTurnTime <= 0)
        {
            NextTurn();
        }

        DisplayTurnUI();

    }

    public void NextTurn()
    {
        onNextTurn.Raise();
    }

    public void TurnNexted()
    {
        if (turnOwner == TurnOwner.Player)
        {
            onBossTurnStart.Raise();
            turnOwner = TurnOwner.Boss;
            realTurnTime = bossTurnTime;

            turnOwnerText.text = "Boss Turn!";
            animator.SetTrigger("Boss' Turn");
        }
        else if (turnOwner == TurnOwner.Boss)
        {
            onPlayerTurnStart.Raise();
            turnOwner = TurnOwner.Player;
            realTurnTime = playerTurnTime;
            turnCount++;

            timeTaken = 0f;

            turnOwnerText.text = "Your Turn!";
            animator.SetTrigger("Player's Turn");
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
