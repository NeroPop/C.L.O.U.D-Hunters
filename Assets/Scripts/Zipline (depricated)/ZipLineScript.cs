using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ZipLineScript : MonoBehaviour
{

    public enum Hands { lefthand, Righthand };
    public Hands hand = Hands.lefthand;

    [HideInInspector] public InputDeviceCharacteristics l_handChar = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left;
    [HideInInspector] public InputDeviceCharacteristics handChar;



    private List<InputDevice> inputDevices = new List<InputDevice>();
    public InputDevice controller;

    private bool inputKeyPressed = false;

    [SerializeField] private float checkOffset = 1f;
    [SerializeField] private float checkRadius = 2f;
    [SerializeField] public bool zipTrue = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (zipTrue == true)
        {

           
        }
    }

    public void ZipTrue()
    {
        zipTrue = true;
    }
}
