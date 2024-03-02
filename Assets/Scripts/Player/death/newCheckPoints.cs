using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newCheckPoints : MonoBehaviour
{
    [SerializeField] private GameObject deathManger;

    [SerializeField] private int checkPointNum;

    [SerializeField] Color myColor;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            deathManger.GetComponent<newDeathSystem>()._activeCheckPoint = checkPointNum;
        }
    }

    public void UpdateCheckpoint()
    {
        deathManger.GetComponent<newDeathSystem>()._activeCheckPoint = checkPointNum;
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = myColor;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
}
