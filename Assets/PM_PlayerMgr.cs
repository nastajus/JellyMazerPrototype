using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tracks the relationship between "player" entities (a game icon) and "gamepad" entities.
//      case 1: player 1 uses gamepad 0 (happy path with normal indexing)
//      case 2: player 2 uses gamepad 3 (skipping two other controllers already recognized)
//      later
//      case X: somehow reconnects a specific gamepad to a specific player if disconnect / reconnect.
//probably should be a single instance (singleton)
//      - should also have a "player" object for each player instance.

public class PM_PlayerMgr : MonoBehaviour
{
    private List<Player> activePlayers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
