using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPreferenceManager : MonoBehaviour
{
    static bool Noob = true;
    static bool LeftHanded = false;

    [SerializeField]
    private UnityEvent PlayIfVRNoob;
    [SerializeField]
    private UnityEvent PlayIfVRPro;

    [SerializeField]
    private UnityEvent LeftHandActive;
    [SerializeField]
    private UnityEvent RightHandActive;

    public void NewToVR()
    {
        Noob = true;
    }

    public void NotNewToVR()
    {
        Noob = false;
    }

    public void SetLeftHand()
    {
        LeftHanded = true;
    }
    public void SetRightHand()
    {
        LeftHanded = false;
    }

    public void TriggerNoob()
    {
        if (Noob)
        {
            PlayIfVRNoob.Invoke();
        }
        else if (!Noob)
        {
            PlayIfVRPro.Invoke();
        }
    }

    public void TriggerLeftHanded()
    {
        if (LeftHanded)
        {
            LeftHandActive.Invoke();
        }
        else if (!LeftHanded)
        {
            RightHandActive.Invoke();
        }
    }
}
