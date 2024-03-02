// Any script can raise an event as long as it has a reference to the GameEvent asset 
// for the event it is raising.

using UnityEngine;

public class ExampleGameEventSender : MonoBehaviour
{
    public GameEvent eventToInvoke; // The GameEvent asset to be raised...

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            //Call "Raise()" on a game event to make it run through the list of delegates and run their actions.
            eventToInvoke.Raise();
        }
    }
}
