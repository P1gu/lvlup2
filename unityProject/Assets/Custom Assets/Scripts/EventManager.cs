using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public static void playPhase1Start() {
        OnPhase1Start();
    }

    public static void playPhase2Start()
    {
        OnPhase2Start();
    }

	public static void SbireKilled() {
		OnUnSbireEstMort ();
	}

	public static void AventurierKilled() {
		OnUnAventurierEstMort();
	}


    public delegate void UnSbireEstMort();
    public static event UnSbireEstMort OnUnSbireEstMort;

    public delegate void UnAventurierEstMort();
    public static event UnAventurierEstMort OnUnAventurierEstMort;

    public delegate void Phase1Start();
    public static event Phase1Start OnPhase1Start;

    public delegate void ReadyForPhase2();
    public static event ReadyForPhase2 OnReadyForPhase2;

    public delegate void Phase2Start();
    public static event Phase2Start OnPhase2Start;

    public delegate void GameOver();
    public static event GameOver OnGameOver;

    public delegate void Victory();
    public static event Victory OnVictory;

    public delegate void EnterMenu();
    public static event EnterMenu OnEnterMenu;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
