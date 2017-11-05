using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// very simple FSM (finite state machine) with only two states, that can only transition between each other.

public class SSS_StartScreenStateMgr : MonoBehaviour {

    public enum StartButton { Disabled, Enabled }

    private StartButton StartButtonState = StartButton.Disabled;

    resultState Transition(currentState, transitionSymbols)
    {
        
    }

    //

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}