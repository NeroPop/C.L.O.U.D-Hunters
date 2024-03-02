using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskTeleport : MonoBehaviour
{
    [SerializeField] private GameObject Disk;
    [SerializeField] private GameObject TeleportPosition;
    private bool DiskGrabbed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!DiskGrabbed)
        {
            Disk.transform.position = TeleportPosition.transform.position;
        }
    }

    public void TeleportDisk()
    {
        if (!DiskGrabbed)
        {
            Disk.transform.position = TeleportPosition.transform.position;
        }
    }

    public void OnDiskGrabbed()
    {
        DiskGrabbed = true;
    }
    public void OffDiskGrabbed()
    {
        DiskGrabbed = false;
    }
}
