using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    [TextArea]
    public string description = "A description of the event.";

    private List<ListenerBase> listeners = new List<ListenerBase>();

    public void Raise()
    {
        Debug.Log("Event Raised : "+this.name);
        for (int i = listeners.Count-1; i>=0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(ListenerBase listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(ListenerBase listener)
    {
        listeners.Remove(listener);
    }
}
