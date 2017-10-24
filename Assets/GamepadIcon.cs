using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Purpose: Class is attached to an image of a gamepad, and is used to represent the state of that gamepad's connectedness.
 *      Tracks instance is connected via a listener, and shows in gray or other color.
 */

public class GamepadIcon : MonoBehaviour
{
    readonly Color32 colorUnconnected_ = new Color32(10, 50, 80, 100);
    readonly Color32 colorConnected_ = new Color32(0, 255, 0, 255);
    //private Color color;      //doesn't work

    private bool connected_ = false;
    private bool Connected
    {
        get { return connected_; }
        set
        {
            connected_ = value;
            UpdateColor(connected_);
        }
    }

	// Use this for initialization
	void Start () {
	    //color = gameObject.GetComponent<Image>().color;  //doesn't work
        UpdateColor(connected_);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateColor(bool connected)
    {
        //color = (connected ? colorConnected_ : colorUnconnected_ );         //doesn't work
        gameObject.GetComponent<Image>().color = (connected ? colorConnected_ : colorUnconnected_);
    }
}
