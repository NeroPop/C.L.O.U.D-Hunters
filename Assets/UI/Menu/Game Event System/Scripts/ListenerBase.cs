using UnityEngine;

public abstract class ListenerBase : MonoBehaviour
{
    public GameEvent Event; //Scriptable object that defines the event and holds a list of delegate functions.
 
    // Add this listener to the list of listeners on the GameEvent object....
    private protected void OnEnable()
    {
        Event.RegisterListener(this);
    }

    // Remove this listener from the list of listeners on the GameEvent object.
    private protected void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    // Called by the GameEvent object when something calls "Raise()".
    public virtual void OnEventRaised()
    {
        Debug.Log("Raising event : "+Event.name);
    }

}
