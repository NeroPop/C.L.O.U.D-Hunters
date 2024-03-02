using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Grabbers;

public class DiskReturnTool : Tool
{
    [Header("Setup")]
    public GameObject disk;
    private ConfigurableJoint conJoint;
    private DiskRecall pathFinder;
    [SerializeField] private float hapticsAmplitude = 0.5f;
    [SerializeField] private HVRHandGrabber rightGrabber;
    [SerializeField] private HVRHandGrabber leftGrabber;

    private bool diskAnchored;

    /// <summary>
    /// Gets the different components required by the Return tool.
    /// </summary>
    public override void ToolInitialize()
    {
        base.ToolInitialize();
        pathFinder = disk.GetComponent<DiskRecall>();

        if (hand == Hands.Righthand)
            pathFinder.SetHandGrabber(rightGrabber);
        else
            pathFinder.SetHandGrabber(leftGrabber);

        Debug.Log(gameObject.name);
    }

    /// <summary>
    /// On Activate
    /// Calls for the DiskConnector to attach the disk to the
    /// anchors configurable joint. Then begins the recall loop
    /// of the 
    /// </summary>
    public override void ToolActivation()
    {
        base.ToolActivation();
        if (!pathFinder.RecallComplete)
        {
            pathFinder.Recall(transform.position);
            controller.SendHapticImpulse(0u, hapticsAmplitude);
        }
        else if (pathFinder.RecallComplete && diskAnchored == true)
        {
            pathFinder.IncrementSet = false;
        }
    }

    public override void ToolDeactivation()
    {
        base.ToolDeactivation();

        pathFinder.SetDiskVelocity();

        if (diskAnchored)
        {
            diskAnchored = false;
        }

        pathFinder.RecallComplete = false;
        pathFinder.PointAlongLine = 0.0f;
        pathFinder.IncrementSet = false;
        controller.SendHapticImpulse(0u, 0);
    }

    public override void ToolUpdate()
    {
        base.ToolUpdate();

        if(hand != handMirror)
        {

            if (hand == Hands.Righthand)
                pathFinder.SetHandGrabber(rightGrabber);
            else
                pathFinder.SetHandGrabber(leftGrabber);

            handMirror = hand;
        }
    }
}
