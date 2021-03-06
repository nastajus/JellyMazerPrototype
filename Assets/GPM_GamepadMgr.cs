﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// logic to detect controllers are connected can go here. 
// use very rudimentary Unity-provided capabilities for this prototype, don't worry about multiple gamepad support just yet.

// also, detect number of GamepadIcons setup in-scene -- because I want to support in-scene positioning of UI elements by developers, 
//without coding their positions & scales & other things that are best left to Editor-manipulations instead. 

public class GPM_GamepadMgr : MonoBehaviour
{
    private string[] gamepadNamesEverConnected;
    private List<Image> gamepadIcons;

    public string[] coupledGamepadChanges { get; set; } //prefer to rewrite event manager to support parameters to avoid this coupling

    void Start () {
	    EM_EventMgr.TriggerEvent("player confirm");
        //gamepadNamesEverConnected = Input.GetJoystickNames();
        gamepadNamesEverConnected = new string[0];

    }
	
	void Update ()
	{
        string[] gamepadChanges = DetectGamepadsConnectedChanged();

        // send message of (gamepadChanges);
	    if (gamepadChanges.Length != 0)
	    {
	        coupledGamepadChanges = gamepadChanges;
            EM_EventMgr.TriggerEvent("update players connected");
	    }
	}

    string[] DetectGamepadsConnectedChanged()
    {
        string[] gamepadNames = Input.GetJoystickNames();
        if (gamepadNames.SequenceEqual(gamepadNamesEverConnected))
        {
            return new string[0];
        }

        List<string> gamepadIndexChanges = new List<string>(); 
        for (int i = 0; i < gamepadNames.Length; i++)
        {
            if (!string.IsNullOrEmpty(gamepadNames[i]))
            {
                Debug.Log("Gamepad " + i + " is connected using: " + gamepadNames[i]);
                gamepadIndexChanges.Add("+" + i);
            }
            else
            {
                Debug.LogWarning("Gamepad " + i + " is disconnected.");
                gamepadIndexChanges.Add("-" + i);
            }
        }
        gamepadNamesEverConnected = gamepadNames;
        return gamepadIndexChanges.ToArray();
    }
}
