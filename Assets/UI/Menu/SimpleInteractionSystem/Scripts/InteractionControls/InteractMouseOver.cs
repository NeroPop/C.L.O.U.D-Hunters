/// ## InteractMouseOver.cs ##
/// Part of the Simple Interaction System
/// By Paul Hedley 20/09/2020
using UnityEngine;
using UnityEngine.Events;

// Simple class that runs a set of actions when the cursor enters the attached collider
// and runs a seperate set of actions when the cursor exits the attached collider.

// If possible, it would be wise to add a "MouseOver" layer for the colliders used on this control with
// no collisions ticked in the physics matrix.

// The object gets disabled on start and waits for something else to reactivate it.
// In the demo system, this is activated and deactivated by the range trigger.
// This is required because the OnMouseEnter / OnMouseExit events have unlimited range.
// In the demo system, this object in turn activates the "Interact Key" object which starts listening for a keypress.
public class InteractMouseOver : MonoBehaviour
{
    public UnityEvent onMouseEnter; // Things to do when the mouse enters the interactive area.
    public UnityEvent onMouseExit; // Things to dfo when the mouse leaves the interaction area.

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        onMouseEnter.Invoke();
    }

    private void OnMouseExit()
    {
        onMouseExit.Invoke();
    }
}
