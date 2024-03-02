using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Disk")
        {
            Destroy(gameObject);
        }
    }
}
