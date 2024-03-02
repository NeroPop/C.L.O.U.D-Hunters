using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseElevatorOnLeave : MonoBehaviour
{
    public ElevatorDoors elevatorDoors;
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(CloseElevator());
    }

    IEnumerator CloseElevator()
    {
        yield return new WaitForSeconds(5);
        elevatorDoors.CloseDoor();
    }
}
