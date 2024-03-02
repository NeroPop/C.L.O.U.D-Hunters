/// ## InteractKey.cs ##
/// Part of the Simple Interaction System
/// By Paul Hedley 20/09/2020
using UnityEngine;
using UnityEngine.Events;

// Simple class to detect key input.
// This class disables the object it is on at start and waits to be activated by
// another part of the interaction system.
// In the demo system this object gets activated and deactivated from the "InteractMouseOver" object.
public class InteractKey : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.F; // Key to detect for
    public UnityEvent onKeyPressed; // Things to do when keypress is detected.

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(interactKey))
        {
            onKeyPressed.Invoke();
        }
    }
}
