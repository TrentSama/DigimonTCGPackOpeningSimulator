using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    public GameEventListener(GameEvent _Event, UnityEvent _UnityEvent)
    {
        this.Event = _Event;
        this.Response = _UnityEvent;
    }


    public void OnEnable()
    {
        if(Event != null)
            Event.RegisterListener(this);
    }
    public void OnDisable()
    {
        if (Event != null)
            Event.UnregisterListener(this);
    }
    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
