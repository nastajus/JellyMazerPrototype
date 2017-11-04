using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// might use to find & store references to multiple canvases, and to initiate switching between fore-ground canvas...  seems good idea.
// possibly rename to CM_CanvasManager...

public class SM_ScreenMgr : MonoBehaviour
{

    private List<GameObject> gamepadIconGos;

    void OnEnable()
    {
        EM_EventMgr.StartListening("get players", ActivateScenesGettingPlayers);
        EM_EventMgr.StartListening("update gamepad icons", UpdateGamepadIconsInScene);
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

    void Start()
    {
        gamepadIconGos = GetGamepadIconsInScene("S1_UI_CANVASES");   //, typeof(GamepadIcon) 
    }

    //dependency on UI. aka exposure, endpoint, interface. 
    List<GameObject> GetGamepadIconsInScene(string sceneName) //S1_UI_CANVASES, typeof(GamepadIcon) 
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene == null) { return null; }

        List<GameObject> rootGos = scene.GetRootGameObjects().ToList();
        List<GameObject> canvasGos = new List<GameObject>();
        foreach (GameObject rootGo in rootGos)
        {
            Canvas[] rootCanvases = rootGo.GetComponentsInChildren<Canvas>();
            if (rootCanvases != null || rootCanvases.Length != 0)
            {
                canvasGos.Add(rootGo);
            }
        }

        List<GameObject> gamepadIconGos = new List<GameObject>();
        foreach (GameObject canvasGo in canvasGos)
        {
            GamepadIcon[] gamepadIcons = canvasGo.GetComponentsInChildren<GamepadIcon>();
            if (gamepadIcons != null || gamepadIcons.Length != 0)
            {
                foreach (GamepadIcon gamepadIcon in gamepadIcons)
                {
                    gamepadIconGos.Add(gamepadIcon.gameObject);
                }
            }
        }

        string output  = "";
        gamepadIconGos.ForEach(x => output += x + ", ");
        Debug.Log(output);

        return gamepadIconGos;
    }

    //void UpdateGamepadIconsInScene(string[] gamepadChanges)   //prefer to add parameter suppported in event meanager
    void UpdateGamepadIconsInScene()
    {
        //access gamepadIcons by-the-way via coupled dependency for time-being
        GPM_GamepadMgr gamepadMgr = gameObject.GetComponent<GPM_GamepadMgr>();
        string[] coupledGamepadChanges = gamepadMgr.coupledGamepadChanges;      //prefer to remove coupling dependency

        for (int i = 0; i < gamepadIconGos.Count && i < coupledGamepadChanges.Length; i++)
        {
            gamepadIconGos[i].GetComponent<GamepadIcon>().Connected = (coupledGamepadChanges[i].Substring(0, 1) == "+") ? true : false;
        }
    }
}
