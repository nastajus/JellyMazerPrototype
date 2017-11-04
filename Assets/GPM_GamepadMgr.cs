﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// logic to detect controllers are connected can go here. 
// use very rudimentary Unity-provided capabilities for this prototype, don't worry about multiple gamepad support just yet.

// also, detect number of GamepadIcons setup in-scene -- because I want to support in-scene positioning of UI elements by developers, 
//without coding their positions & scales & other things that are best left to Editor-manipulations instead. 

public class GPM_GamepadMgr : MonoBehaviour
{
    private string[] gamepadNamesEverConnected; 

    void Start () {
	    EM_EventMgr.TriggerEvent("player confirm");
    }
	
	void Update ()
	{
        DetectGamepadsConnectedChanged();
	}

    //dependency on UI. aka exposure, endpoint, interface. 
    void CountGamepadIconsInScene()
    {
        
    }

    void UpdateGamepadIconsInScene()
    {
        
    }

    void DetectGamepadsConnectedChanged()
    {
        string[] gamepadNames = Input.GetJoystickNames();
        if (gamepadNamesEverConnected == null)
        {
            gamepadNamesEverConnected = Input.GetJoystickNames();
            return;
        }

        if (gamepadNames.SequenceEqual(gamepadNamesEverConnected))
        {
            return;
        }

        for (int i = 0; i < gamepadNames.Length; i++)
        {
            if (!string.IsNullOrEmpty(gamepadNames[i]))
            {
                Debug.Log("Gamepad " + i + " is connected using: " + gamepadNames[i]);
            }
            else
            {
                Debug.LogWarning("Gamepad " + i + " is disconnected.");
            }
        }
        gamepadNamesEverConnected = gamepadNames;
    }
}
