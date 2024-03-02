using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is just an example of how a script could raise an event.
// The raise function can get called directly on the scriptable object without needing a monobehaviour like this.
public class RaiseGameEvent : MonoBehaviour
{
    public GameEvent eventToCall;

    public void RaiseEvent()
    {
        eventToCall.Raise();
    }
}
