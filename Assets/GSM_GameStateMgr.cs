using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSM_GameStateMgr : MonoBehaviour
{

    private SSS_StartScreenStateMgr startScreenStateMgr_;
    private MSS_MazeScreenStateMgr mazeScreenStateMgr_;
    private CSS_CreditsScreenStateMgr creditsScreenStateMgr_;

    enum GameStates
    {
        StartScreen, MazeScreen, CreditsScreen
    }

    // Use this for initialization
    void Start ()
    {
        InitStates();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitStates()
    {
        //add a bunch of startup logic (other classes, other components).
        startScreenStateMgr_ = gameObject.AddComponent<SSS_StartScreenStateMgr>();
        mazeScreenStateMgr_ = gameObject.AddComponent<MSS_MazeScreenStateMgr>();
        creditsScreenStateMgr_ = gameObject.AddComponent<CSS_CreditsScreenStateMgr>();
    }

}
