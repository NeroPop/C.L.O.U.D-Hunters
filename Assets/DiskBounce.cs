using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBounce : MonoBehaviour

{
    public float bounceSpeed = 10f;
    public float diskThrust = 100f;
    public bool doesBounce = false;
   
    Rigidbody disk;
    // Start is called before the first frame update
    void Start()
    {
        disk = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // disk.velocity = disk.AddForce(transform.up * ) ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        if (disk.velocity.sqrMagnitude <= bounceSpeed)
        {
            Debug.Log("i have hit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    
}
