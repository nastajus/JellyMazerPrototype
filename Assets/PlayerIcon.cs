using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purpose: like the "GamepadIcon" class, this updates a game object to be representative of the player's connected state.

public class PlayerIcon : MonoBehaviour
{
    private const float rotationSpeed_ = 50;

    public List<Sprite> prototypeQuickActiveSprites;   //used to load player bean icons quickly by dragging in Editor.
    public Sprite prototypeQuickInactiveSprite;

    private static int playersSpawned_ = 0;  //outside this rapid prototype, this would be initialized elsewhere & referenced here, or something.    
    private int playerNum_ = 0;             //logic will use starting-index of 1, here 0 means unitialized.


    readonly Color32 colorUnselected_ = new Color32(127, 127, 127, 127);    //duplicated from GamepadIcon... again it's for rapid dev.
    readonly Color32 colorSelected_ = new Color32(0, 255, 0, 255);

    private bool selected_ = false;
    private bool Selected
    {
        get { return selected_; }
        set
        {
            selected_ = value;
            UpdateColor(selected_);
        }
    }

    // Use this for initialization
    void Start ()
    {
        playersSpawned_ = (playersSpawned_ + 1) % prototypeQuickActiveSprites.Count; //counting resources is NOT how i want to govern # players... this is a rapid prototype
        playerNum_ = playersSpawned_;
        SetColorfulSprite();
    }
	
	void Update ()
	{
        RotatePlayerIcon();
	    UpdateColor(selected_);
	}

    void RotatePlayerIcon()
    {
        if (!selected_) return;

        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed_);
    }

    void SetColorfulSprite()
    {
        if (playerNum_ <= 0) return; //unintended state... just a safety precaution 

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = prototypeQuickActiveSprites[playerNum_ - 1];
    }

    //also duplicated from GamepadIcon...
    void UpdateColor(bool selected)
    {
        gameObject.GetComponent<SpriteRenderer>().color = (selected ? colorSelected_ : colorUnselected_);
    }

    //arguably pointless, since this is already saved in the prefab... ... the tensions between Editor & code increase.
    void SetBlandSprite()
    {
        if (!prototypeQuickInactiveSprite) return; //simple guard cond... pointless?

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        //Sprite sp = sr.sprite;
        //sp = prototypeQuickActiveSprites[1];      //doesn't work
        sr.sprite = prototypeQuickInactiveSprite;
    }

}
