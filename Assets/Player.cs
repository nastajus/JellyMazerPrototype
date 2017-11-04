using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//track 

public class Player : MonoBehaviour
{
    private int gamepad;
    private bool gamepadIsConnected;    //no justification yet for a gamepad class itself.   this bool is sufficient to compress here.
    //no button mapping necessary... 
        //just start, 4 directions, "use", "back / cancel".
            // "start action" can be used by anyone anytime, if sufficient game state advancement has occurred to permit it.
            // that is, it's globally executable by any player.
            // whereas all other controls are player-specific. 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
