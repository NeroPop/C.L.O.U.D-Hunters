using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TouchPress : MonoBehaviour
{
    public UnityEvent pressed;
    public string targetTag;

    [ContextMenu("Press")]
    protected virtual void ButtonPress()
    {
        pressed.Invoke();
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            ButtonPress();
        }
    }
}