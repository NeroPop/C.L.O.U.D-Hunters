using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public GameObject targetObject;

    public void Toggle()
    {
        // All this does is set the active state of the target game object
        // to whichever state it is not currently in.
        targetObject.SetActive(!targetObject.activeSelf);
    }
}
