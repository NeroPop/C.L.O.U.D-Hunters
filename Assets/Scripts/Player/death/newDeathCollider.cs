using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class newDeathCollider : MonoBehaviour
{
    [SerializeField] private GameObject deathManger;

    [SerializeField] Color myColor;

    [SerializeField]
    private UnityEvent OnKill;

    void OnTriggerEnter(Collider other)
    {
       // if (other.CompareTag("Player"))
     //   {
            //Debug.Log("Dead");
        deathManger.GetComponent<newDeathSystem>().Death();
        OnKill.Invoke();
        //  }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = myColor;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
}