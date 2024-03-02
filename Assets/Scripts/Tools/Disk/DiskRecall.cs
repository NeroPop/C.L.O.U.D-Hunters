using HurricaneVR.TechDemo.Scripts;
using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskRecall : MonoBehaviour
{
    // Has the disk reached the hand
    private bool recallComplete;
    public bool RecallComplete
    {
        get
        {
            return recallComplete;
        }

        set
        {
            recallComplete = value;
        }
    }
    
    // The point along the path the disk is following
    private float pointAlongLine = 0f;
    public float PointAlongLine
    {
        set
        {
            pointAlongLine = value;
        }
    }
    const float endAmount = 1.0f; // The end of the line

    // Incremental Settings:
    // The increment between 0 and 1 which determines how
    // quickly the disk moves along the path. Determined by
    // target distance and distanceDivisions.
    private float incrementAmount = 0.0f;
    private bool incrementSet = false;
    public bool IncrementSet
    {
        set
        {
            incrementSet = value;
        }
    }
    
    private Vector3 recallStartPos;

    private float recallTime = 0.0f;

    private Rigidbody rigidbody;

    [Header("Grab Distance")]
    [SerializeField] private float distanceToGrab = 0.5f; // The distance the object has to be from the hand to snap grab

    [Header("Disk Speed")]
    [SerializeField] private float diskSpeed = 1.0f; // The distance in unity units between a division in the line
    public enum MeasurementUnit { Millimetres, Centimetres }
    [SerializeField] private MeasurementUnit measurementUnit;

    [HideInInspector] public DemoCodeGrabbing grabber;

    private void Awake()
    {
        grabber = GetComponent<DemoCodeGrabbing>();
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Recalls the disk towards the players hand by an increment
    /// set in GetIncrement(). This loop will run until the recall
    /// has been completed as determined by the distance check in the
    /// second if statement. If the disk is within distance the disk
    /// will be snapped to the players hand.
    /// </summary>
    /// <param name="target"></param>
    public void Recall(Vector3 target)
    {
        Debug.Log("Recalling");
        if (!incrementSet)
        {
            rigidbody.velocity = Vector3.zero;
            incrementAmount = GetIncrement(target);
            incrementSet = true;
            recallStartPos = transform.position;
        }

        if (!recallComplete)
        {
            Vector3 position = CalculateCurrentPosition(target);

            transform.position = position;

            pointAlongLine += incrementAmount;
            //Debug.Log(pointAlongLine);

            if (Vector3.Distance(transform.position, target) <= distanceToGrab)
            {
                Debug.Log("Ended Recall");
                recallComplete = true;
                pointAlongLine = 0.0f;
                grabber.Grab();
                recallTime = 0.0f;
            }

            recallTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// Interopolates the disk between its starting position and
    /// and the players hand.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private Vector3 CalculateCurrentPosition(Vector3 target)
    {
        Vector3 newPosition;

        newPosition = Vector3.Lerp(recallStartPos, target, pointAlongLine);

        /*float posX = (1 - pointAlongLine) * transform.position.x + pointAlongLine * target.x;
        float posY = (1 - pointAlongLine) * transform.position.y + pointAlongLine * target.y;
        float posZ = (1 - pointAlongLine) * transform.position.z + pointAlongLine * target.z;*/
;
        return newPosition;
    }

    /// <summary>
    /// Gets the increment data by making divisions in the line
    /// using distanceBetweenDivisions and dividing the end increment
    /// by it.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private float GetIncrement(Vector3 target)
    {
        float increment = 0.0f;
        float distance = Vector3.Distance(transform.position, target);
        //int divisors = (int)(distance / (diskSpeed / (measurementUnit == MeasurementUnit.Centimetres ? 100 : 1000)));
        increment = endAmount / (distance / (diskSpeed / (measurementUnit == MeasurementUnit.Centimetres ? 100 : 1000)));
        //Debug.Log(increment);

        return increment;
    }

    public void SetDiskVelocity()
    {
        if(recallComplete == false)
        {
            Vector3 velocity;

            float displacementX = recallStartPos.x - transform.position.x;
            float displacementY = recallStartPos.y - transform.position.y;
            float displacementZ = recallStartPos.z - transform.position.z;

            float velocityX = displacementX / recallTime;
            float velocityY = displacementY / recallTime;
            float velocityZ = displacementZ / recallTime;

            velocity = new Vector3(-velocityX, -velocityY, -velocityZ);
            rigidbody.velocity = velocity;
        }
    }

    public void SetHandGrabber(HVRHandGrabber handGrabber)
    {
        Debug.Log(handGrabber.name);
        grabber.Grabber = handGrabber;
    }
}
