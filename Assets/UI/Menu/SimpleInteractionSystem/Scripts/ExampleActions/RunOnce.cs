using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunOnce : MonoBehaviour
{
    public UnityEvent actions;
    bool hasRun = false;
    public void Run()
    {
        if(hasRun==false)
        {
            hasRun = true;
            actions.Invoke();
        }
    }
}
