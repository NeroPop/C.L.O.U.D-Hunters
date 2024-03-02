using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRNoob : MonoBehaviour
{
    static bool Noob = true;
    static bool LeftHand = false;

    [SerializeField]
    private UnityEvent PlayIfVRNoob;
    [SerializeField]
    private UnityEvent PlayIfVRPro;

    public void NewToVR()
    {
        Noob = true;
    }

    public void NotNewToVR()
    {
        Noob = false;
    }

    public void TriggerNoob()
    {
        if (Noob)
        {
            PlayIfVRNoob.Invoke();
        }
        else
        {
            PlayIfVRPro.Invoke();
        }
    }

}
