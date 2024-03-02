using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerZipLogic : MonoBehaviour
{
    
    public enum Hands {lefthand, Righthand };
    public Hands hand = Hands.lefthand;

    [HideInInspector] public InputDeviceCharacteristics l_handChar = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left;
    [HideInInspector] public InputDeviceCharacteristics handChar;

    

    private List<InputDevice> inputDevices = new List<InputDevice>();
    public InputDevice controller;

    private bool inputKeyPressed = false;

    [SerializeField] private float checkOffset = 1f;
    [SerializeField] private float checkRadius = 2f;
    [SerializeField] public bool zipTrue = false;
    [SerializeField] public bool example = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (zipTrue == true)
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0, checkOffset, 0), checkRadius, Vector3.up);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag == "ZipLine")
                {
                    hit.collider.GetComponent<Zipline>().StartZipLine(this.gameObject);
                }
            }
        }*/
        
        if (!controller.isValid)
        {
            newInputDevice();
        }
        else
        {
            if (GetKey() && !inputKeyPressed)
            {
                inputKeyPressed = true;
                sphereCast();
            }

            else 
            {
                //Debug.Log("Dosent work lol");
            }
        }
        /// this is where pair programming is required
       

        
    }
    
    void newInputDevice()
    {
        InputDevices.GetDevicesWithCharacteristics(l_handChar, inputDevices);
        Debug.Log(inputDevices.Count);

        if (inputDevices.Count > 0)
        {
            controller = inputDevices[0];
            Debug.Log("Controller found");
        }

    }
    
    
    public virtual bool GetKey()
    {
        bool keyPressed;
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out keyPressed);
        if (keyPressed)
        {
            return true;
        }

        return false;
    }
    
    void sphereCast()
    {

        RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0, checkOffset, 0), checkRadius, Vector3.up);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "ZipLine")
            {
                hit.collider.GetComponent<Zipline>().StartZipLine(this.gameObject);
            }
        }

    }

    public void ZipTrue()
    {
        zipTrue = true;
    }
}
