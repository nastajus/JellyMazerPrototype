using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manually attach this class in-Editor singular function StartButton to the Button's OnClick selection popup, with setting as Editor & Runtime. 
// rename to StartButtonManager.
// pushes out event : "start game"
// receives events to toggle on/off with "can start game" + "cannot start game".

public class UIA_UIAdapter : MonoBehaviour {

    void Start()
    {
        
    }

    //"adapter method" that is exposed / bound in-Editor to the Button's OnClick. 
    public void StartButton()
    {
        
    }

}
