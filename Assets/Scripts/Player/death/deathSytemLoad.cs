using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathSytemLoad : MonoBehaviour
{

    [SerializeField] private GameObject deathSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            deathSystem.GetComponent<newDeathSystem>().setSystem();
            other.gameObject.GetComponent<respawnManger>().newDeathSystem = deathSystem;
        }
    }
}
