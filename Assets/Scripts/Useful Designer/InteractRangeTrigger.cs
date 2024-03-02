/// ## InteractionRangeTrigger.cs ##
/// Part of the Simple Interaction System
/// By Paul Hedley 20/09/2020
using UnityEngine;
using UnityEngine.Events;

// A very simple class that performs a set of actions when anything enters the an attached trigger
// and performs a seperate set of actions whenever anything exits the trigger.

// It will be up to you to decide how to make the trigger know that the object is a player.
// I recommend using layers for maximum efficiency and no extra coding!
// Set up "Player" and "PlayerTrigger" layers in the project and set the physics matrix 
// so that the "PlayerTrigger" layer only collides with the "Player" layer.

// Alternatively you could use tags.  I wouldn't reccomend this for a single player interaction, but it would work with
// only minor code alterations.  In scenarios other than a player interaction 
// (eg many different things could activate this control) using tags to identify the type of object would work.
public class InteractRangeTrigger : MonoBehaviour
{

    public UnityEvent onEnterRange; // Things to do when something enters the trigger.
    public UnityEvent onExitRange; // Things to do when something exits the trigger.


    private void OnTriggerEnter(Collider other)
    {
        onEnterRange.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        onExitRange.Invoke();
    }
}
