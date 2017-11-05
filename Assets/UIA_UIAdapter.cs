using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// manually attach this class in-Editor singular function PermitState to the Button's OnClick selection popup, with setting as Editor & Runtime. 
// rename to StartButtonManager / SB_StartButtonMgr.
// pushes out event : "make maze"
// receives events to toggle on/off with "can start game" + "cannot start game".

public class UIA_UIAdapter : MonoBehaviour {

    void OnEnable()
    {
        EM_EventMgr.StartListening("enable start button", ToggleStartButtonOn);
        EM_EventMgr.StartListening("disable start button", ToggleStartButtonOff);
    }

    void OnDisable()
    {
        EM_EventMgr.StopListening("enable start button", ToggleStartButtonOn);
        EM_EventMgr.StopListening("disable start button", ToggleStartButtonOff);
    }

    void ToggleStartButtonOn()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }

    void ToggleStartButtonOff()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }


    //"adapter method" that is exposed / bound in-Editor to the Button's OnClick. 
    //perhaps in the future create an interface which enforces this, by throwing an exception, by detecting when a singular function isn't bound
    public void StartButton()
    {
        EM_EventMgr.TriggerEvent("make maze");
    }

    void Start()
    {
        ToggleStartButtonOff();
    }
}
