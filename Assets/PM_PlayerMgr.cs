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
    //private List<Player> activePlayers;


    public int coupledPlayersActive { get; private set; }   //prefer other solution using event messaging with # parameter passed instead.
    public const int MINIMUM_PLAYERS = 2;


    void OnEnable () {
        EM_EventMgr.StartListening("update players connected", UpdatePlayerActiveCount);

    }

    void OnDisable () {
        EM_EventMgr.StopListening("update players connected", UpdatePlayerActiveCount);

    }

    void UpdatePlayerActiveCount()
    {
        //access gamepadIcons by-the-way via coupled dependency for time-being
        GPM_GamepadMgr gamepadMgr = gameObject.GetComponent<GPM_GamepadMgr>();
        string[] coupledGamepadChanges = gamepadMgr.coupledGamepadChanges;      //prefer to remove coupling dependency

        int players = 0;
        for (int i = 0; i < coupledGamepadChanges.Length; i++)
        {
            bool active = coupledGamepadChanges[i].Substring(0, 1) == "+";
            if (active)
            {
                players++;
            }
        }
        coupledPlayersActive = players;
    }

}
