using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Zipline : MonoBehaviour
{
    [SerializeField] private Zipline targetZip;
    [SerializeField] private float zipSpeed = 5f;
    [SerializeField] private float zipScale= 0.2f;

    [SerializeField] private float arrivalThreshold = 0.4f;


    public Transform ZipTransform;

    private bool zipping = false;
    private GameObject localZip;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!zipping || localZip == null) return;

        localZip.GetComponent<Rigidbody>().AddForce((targetZip.ZipTransform.position - ZipTransform.position).normalized * zipSpeed * Time.deltaTime, ForceMode.Acceleration);

        if (Vector3.Distance(localZip.transform.position,targetZip.ZipTransform.position) <= arrivalThreshold)
        {
            ResetZipLine();
        }
    }

    public void StartZipLine(GameObject player)
    {
        if (zipping) return;

        localZip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        localZip.transform.position = ZipTransform.position;
        localZip.transform.localScale = new Vector3(zipScale, zipScale, zipScale);
        localZip.AddComponent<Rigidbody>().useGravity = false;
        localZip.GetComponent<Collider>().isTrigger = true;

        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //here you would also want to prevent the player from moving by disaplying their input and control, but dont know how feasible that will be in vr.
        //
        player.transform.parent = localZip.transform;
        zipping = true;
    }

    private void ResetZipLine()
    {
        if (!zipping) return;

        GameObject player = localZip.transform.GetChild(0).gameObject;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        player.transform.parent = null;
        Destroy(localZip);
        localZip = null;
        zipping = false;
        Debug.Log("Zipline reset");
    }
}
