using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour
{
    public Transform playerObj;
    protected NavMeshAgent companionMesh;

    // Start is called before the first frame update
    void Start()
    {
        companionMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        companionMesh.SetDestination(playerObj.position);
    }
}
