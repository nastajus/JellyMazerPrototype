using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//purpose: administer all events that drive this application: 
/*
 *  - "get players"
 *  - "can start game" + "cannot start game"
 *  - "start game" 
 */

public class EM_EventMgr : MonoBehaviour
{

    private Dictionary<string, UnityEvent> Events_;

    private static EM_EventMgr EventManager_;

    public static EM_EventMgr Instance
    {
        get
        {
            if (!EventManager_)
            {
                //i'm calling this the "static workaround"
                EventManager_ = FindObjectOfType(typeof(EM_EventMgr)) as EM_EventMgr;
                
                //not valid to use "this" in static
                //EventManager_ = this;

                if (!EventManager_)
                {
                    Debug.LogError("there needs to be one active EM_EventMgr script on a gameobject in your scene.");
                }
                else
                {
                    //it's necessary to call another method to bridge the gap between static & instance variables.
                    //which begs this question to ponder over: when would having an instance event dictionary be valuable?
                    EventManager_.Init();
                }
            }
            return EventManager_;
        }
    }

    void Init()
    {
        if (Events_ == null)
        {
            Events_ = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listenerFP)
    {
        // we need to create a new unity event
        UnityEvent thisEvent = null;
        // because we want to make sure there is a key-value pair there

        //TryGetValue is at least as fast, or faster, than ContainsKey
        if (Instance.Events_.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listenerFP);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listenerFP);
            Instance.Events_.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listenerFP)
    {
        //if we've destroyed or not found our event manager, we want to make sure we don't get a null exception
        if (EventManager_ == null) return;

        UnityEvent thisEvent = null;
        if (Instance.Events_.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listenerFP);
        }
    }

    //we finally need something that's going to *trigger* it
    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.Events_.TryGetValue(eventName, out thisEvent))
        {
            //we'll simply invoke the event (affects *ALL* listeners)
            thisEvent.Invoke();
        }
        else
        {
            Debug.LogWarning("Event name: " + eventName + " was fired but nothing was listening for it!");
        }
    }
}
