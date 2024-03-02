using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeEvent : MonoBehaviour
{
    public UnityEvent Onstart;
    public UnityEvent OnUpdate;


    // Start is called before the first frame update
    void Start()
    {
        Onstart.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate.Invoke();
    }
}
