using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPM_GamepadMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    EM_EventMgr.TriggerEvent("player confirm");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
