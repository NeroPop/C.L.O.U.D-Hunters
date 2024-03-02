using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Tool : MonoBehaviour
{
    public enum Hands { Lefthand, Righthand };
    public Hands hand = Hands.Lefthand;
    public Hands handMirror;
    

    [HideInInspector] public InputDeviceCharacteristics r_handCharacteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right;
    [HideInInspector] public InputDeviceCharacteristics l_handCharacteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left;
    [HideInInspector] public InputDeviceCharacteristics handCharacteristics;

    private List<InputDevice> inputDevices = new List<InputDevice>();
    public InputDevice controller;

    private bool inputKeyPressed = false;

    [Header("Testing Tools")]
    [SerializeField] private bool activateTool;
    [SerializeField] private bool disableControllers;

    private void Start()
    {
        handCharacteristics = hand == Hands.Lefthand ? l_handCharacteristics : r_handCharacteristics;
        handMirror = hand;
        ToolInitialize();
        //Debug.Log(handCharacteristics);
    }

    private void FixedUpdate()
    {
        if(!controller.isValid && !disableControllers)
        {
            //Debug.Log("Find Input Device");
            FindInputDevice();
        }
        else
        {
            if (GetActivationKey() || activateTool)
            {
                inputKeyPressed = true;
                ToolActivation();
            }
            else if (!GetActivationKey() && inputKeyPressed)
            {
                inputKeyPressed = false;
                ToolDeactivation();
            }
        }

        ToolUpdate();
    }


    public void FindInputDevice()
    {
        InputDevices.GetDevicesWithCharacteristics(handCharacteristics, inputDevices);
        //Debug.Log(inputDevices.Count);

        if(inputDevices.Count > 0)
        {
            controller = inputDevices[0];
            Debug.Log("Controller Found");
        }
    }

    public virtual bool GetActivationKey()
    {
        bool keyPressed;
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out keyPressed);
        if (keyPressed)
        {
            return true;
        }

        return false;
    }

    public virtual void ToolInitialize()
    {

    }

    public virtual void ToolActivation()
    {
        
    }

    public virtual void ToolDeactivation()
    {
        
    }

    public virtual void ToolUpdate()
    {

    }
}
