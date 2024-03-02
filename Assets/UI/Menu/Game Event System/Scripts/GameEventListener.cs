using UnityEngine;
using UnityEngine.Events;
// Add one of these scripts to an object for each event it should respond to.
public class GameEventListener : ListenerBase
{
    //public GameEvent Event; //Scriptable object that defines the event and holds a list of delegate functions.
    public UnityEvent Response; // UnityEvent that stores the actions to perform when the event is raised.

    // Called by the GameEvent object when something calls "Raise()".
    public override void OnEventRaised()
    {
        Response.Invoke();
    }


}
