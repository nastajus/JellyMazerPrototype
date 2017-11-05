using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

// Purpose : this class manages overall executive state of the game, and initiates other classes / components / scenes / events

// pseudo message flow: 

// 1. make itself a singleton -- DONE
// 2. initialize the event management system, all (?)  inter-class communication will (?) be via messaging. -- DONE
// 3. initialize SM_ScreenMgr, make it listen & wait.
// 4. here use local enum to track various game states, set to "unitialized" state to begin.
// 5. here fire off message to SM_ScreenMgr to enter state "get players"(), which in turn will cause loading both S1 scenes in tandem addititively. (also probably with EM)
//      5b. consequently, above will cause PM_PlayerMgr to initialize, listen & wait.
//      5c. PM_PlayerMgr will receive message to "get players"(), which will cause GPM_GamepadMgr to listen for controllers
//      5d. GPM_GamepadMgr will look in an Update loop continuously for three essential tasks: 
//              (i) detect any gamepads attached, and do somehow update the visual representation, probably directly messaging each "GamepadIcon" object.
//              (ii) detect any button input at all from these gamepad, and use that to toggle the state of being an active player or not; probably direct message "PlayerIcon" object to spin and light up.
//              (iii) for every "player confirm"(#) event triggered by any gamepad it will also fire a message off, intending for the GM_GameMgr to capture & react to.
// 6. the GM_GameManager captures the "player confirm"(#) message from 5d.(iii)., and validates against it's own game logic if enough players are available to allow playing. if yes, changes game state enum to the following, and sends message to "getting players & can start game" (referred to as just ~"can start game"~ ).  similarly will be listening for unconfirms & do the reverse, reverting back game state enum to just "get players" if #s drop below game rules.
// 7. the SM_ScreenManager captures the message ~"can start game"~ and visually enables (brightens) the "Start Game" button. if that button is clicked via mouse, it will send a message "new maze".
// 8. the GM_GameManager captures the "make maze", which then advances it's local enum state accordingly, and then it sends a message to SM_ScreenMgr to somehow hide old canvas and (load an arbitrary timer canvas... don't actually care about this... it's a placeholder). So SM is listening for "make maze" in order to transition away from "getting players" UI canvas and instead enable ~playing-in-maze~ UI canvas in it's place.
// 9. the MM_MazeMgr will run, it will make a really REALLY SIMPLE map (not even a maze), where it simply spawns user-controllable placeholder gameobjects, in a designated SpawnLocation (can all overlap for now). 
//      9b. MM_MazeMgr will generate a simple layout of tiles as a temporary placeholder, a start location on the left, an end location on the right.
//      9c. MM_MazeMgr will create spawn OnTriggerEnter region to detect end-level entry of players, and fire message "player exited"(#), which the GM_GameMgr will detect.
// 10. GM_GameMgr will detect players have exited. Depending on game mode, it will have logic to handle accordingly. for now, let's just say any player that exits triggers "new map", and that this happens twice, and then credits roll.


public class GM_GameMgr : MonoBehaviour //, IEL_IEventListeners
{
    public static GM_GameMgr Instance = null;

    private EM_EventMgr EM_EventMgr_;
    private SM_ScreenMgr SM_ScreenMgr_;
    private GPM_GamepadMgr GPM_GamepadMgr_;
    private PM_PlayerMgr PM_PlayerMgr_;
    private GSM_GameStateMgr GSM_GameStateMgr_;
    private MM_MazeMgr MM_MazeMgr_;

    //private UnityAction listenerPlayerConfirmed;
    //private UnityAction listenerEtc;
    private UnityAction listenerMakeMaze;

    void Awake ()
    {
        InitSingleton();
        InitListeners();
        InitGame();
    }

    void InitSingleton()
    {
        //check if instance already exists
        if (Instance == null)
        {
            Instance = this; //if not, set instance to "this".
        }

        //if instance already exists, and it's not "this":
        else if (Instance != this)
        {
            //then destroy this, enforcing singleton pattern.
            Destroy(gameObject);
        }

        //sets to not destroy when reloading scenes
        DontDestroyOnLoad(gameObject);
    }

    void InitListeners()
    {
        EM_EventMgr_ = gameObject.AddComponent<EM_EventMgr>();
        //listenerPlayerConfirmed = new UnityAction(PlayerConfirmFunction);
        //listenerEtc = new UnityAction(EtcFunction);
        listenerMakeMaze = new UnityAction(MakeMaze);

    }

    void InitGame()
    {
        //add a bunch of startup logic (other classes, other components).
        SM_ScreenMgr_ = gameObject.AddComponent<SM_ScreenMgr>();
        GPM_GamepadMgr_ = gameObject.AddComponent<GPM_GamepadMgr>();
        PM_PlayerMgr_ = gameObject.AddComponent<PM_PlayerMgr>();
        GSM_GameStateMgr_ = gameObject.AddComponent<GSM_GameStateMgr>();
        MM_MazeMgr_ = gameObject.AddComponent<MM_MazeMgr>();

        EM_EventMgr.TriggerEvent("get players");
    }


    void OnEnable()
    {
        //EM_EventMgr.StartListening("player confirm", PlayerConfirmFunction);
        //EM_EventMgr.StartListening("etc", EtcFunction);
        EM_EventMgr.StartListening("make maze", MakeMaze);
    }

    //OnDisable is necessary to manage properly memory and thus avoid memory leaks
    void OnDisable()
    {
        //EM_EventMgr.StopListening("player confirm", PlayerConfirmFunction);
        //EM_EventMgr.StopListening("etc", EtcFunction);
        EM_EventMgr.StopListening("make maze", MakeMaze);
    }

    /*
    void PlayerConfirmFunction()
    {
        Debug.Log("player confirm -- Something happened!");
    }

    void EtcFunction()
    {
        Debug.Log("etc");
    }
    */

    void MakeMaze()
    {
        Debug.Log("making ze maze, sir.");
    }
}
