using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class ElevatorDoors : MonoBehaviour
{
    public DOTweenAnimation animL;
    public DOTweenAnimation animR;
    public DOTweenAnimation elevatorMovingAnim;
    public bool doorOpen = false;
    bool isOpen = false;

    public UnityAction onDoorClose;
    public GameObject doors_Open;
    public GameObject doors_Closed;

    [ContextMenu("Open")]
    public void OpenDoor()
    {
        //if (doorOpen)
        //{
        //    CloseDoor();
        //    return;
        //}

        Debug.Log("Opening Doors");
        //animL.DORewind();
        //animR.DORewind();
        animL.DOPlayForward();
        animR.DOPlayForward();
        doorOpen = true;
        doors_Open.SetActive(true);
        doors_Closed.SetActive(false);
    }

    [ContextMenu("Close")]
    public void CloseDoor()
    {
        Debug.Log("Closing Doors");
        animL.DOPlayBackwards();
        animR.DOPlayBackwards();
        doorOpen = false;
        doors_Open.SetActive(false);
        doors_Closed.SetActive(true);
    }

    [ContextMenu("ElevatorMove")]
    public void ElevatorMove()
    {
        CloseDoor();
        if (!doorOpen)
        {
            Debug.Log("Elevator is Moving");
            elevatorMovingAnim.DOPlayForward();
            doorOpen = true;
        }
    }

    public void OnDoorClose()
    {
        Debug.Log("Elevator complete");
        onDoorClose.Invoke();
    }
}