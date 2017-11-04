using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// might use to find & store references to multiple canvases, and to initiate switching between fore-ground canvas...  seems good idea.
// possibly rename to CM_CanvasManager...

public class SM_ScreenMgr : MonoBehaviour {

    void OnEnable()
    {
        EM_EventMgr.StartListening("get players", ActivateScenesGettingPlayers);
    }

    void OnDisable()
    {
        EM_EventMgr.StopListening("get players", ActivateScenesGettingPlayers);
    }

    void ActivateScenesGettingPlayers()
    {
        //for now i'll just hard-code the sole scene here... and re-evaluate later if i want to refactor this elsewhere.
        SceneManager.LoadScene("S1_UI_CANVASES", LoadSceneMode.Additive);
        SceneManager.LoadScene("S1_GO_GET_PLAYERS", LoadSceneMode.Additive);
    }

    //dependency on UI. aka exposure, endpoint, interface. 
    public int CountGamepadIconsInSceneS1()
    {
        Scene scene = SceneManager.GetSceneByName("S1_UI_CANVASES");
        if (scene == null) { return 0; }
        return 1;

    }

}
