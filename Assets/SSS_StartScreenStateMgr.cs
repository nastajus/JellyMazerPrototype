using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// very simple FSM (finite state machine) with only two states, that can only transition between each other.

public class SSS_StartScreenStateMgr : MonoBehaviour {

    public enum PermitState { Disabled, Enabled }
    private enum NumberPlayers { Enough, Not_Enough }

    //Dictionary<NumberPlayers, string> 

    //private static string MESSAGE_; 

    private PermitState StartButtonState_ = PermitState.Disabled;

    //examining current state only prevents unnecessary computation
    //this seems overly wordy..
    PermitState TransitionToNewState(PermitState currentState, NumberPlayers sufficientNumberPlayers)
    {
        if (currentState == PermitState.Disabled && sufficientNumberPlayers == NumberPlayers.Enough)
        {
            return PermitState.Enabled; 
        }
        if (currentState == PermitState.Enabled && sufficientNumberPlayers == NumberPlayers.Not_Enough)
        {
            return PermitState.Disabled;
        }
        return currentState;
    }

    void OnEnable () {
        EM_EventMgr.StartListening("update players connected", VerifySufficientAmountPlayersToBegin);
    }

    void OnDisable () {
        EM_EventMgr.StopListening("update players connected", VerifySufficientAmountPlayersToBegin);
    }

    void VerifySufficientAmountPlayersToBegin()
    {
        PM_PlayerMgr playerMgr = gameObject.GetComponent<PM_PlayerMgr>();
        if (playerMgr == null) { return; }

        if (playerMgr.coupledPlayersActive >= PM_PlayerMgr.MINIMUM_PLAYERS)
        {
            PermitState resultState = TransitionToNewState(StartButtonState_, NumberPlayers.Enough);
            if (resultState != StartButtonState_)
            {
                StartButtonState_ = resultState;
                EM_EventMgr.TriggerEvent("enable start button");
            }
        }

        else if (playerMgr.coupledPlayersActive < PM_PlayerMgr.MINIMUM_PLAYERS)
        {
            PermitState resultState = TransitionToNewState(StartButtonState_, NumberPlayers.Not_Enough);
            if (resultState != StartButtonState_)
            {
                StartButtonState_ = resultState;
                EM_EventMgr.TriggerEvent("disable start button");
            }
        }
    }
}