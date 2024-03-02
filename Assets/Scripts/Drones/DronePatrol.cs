// <copyright file="DronePatrol.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Drone.Patrol
{
    using Drone.StateMachine;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary><c>DronePatrol</c> handles the drone patrolling state.</summary>
    public class DronePatrol : MonoBehaviour
    {
        [Header("Patrol Settings")]
        [SerializeField] 
        private float droneSpeed;

        private float speedSave;

        [SerializeField]
        private float startSpeed;

        [SerializeField]
        private float rotationSpeed;

        [SerializeField] 
        private float distanceToTargetCompletion = 0.2f;

        [SerializeField] 
        private Vector3 rotationOffset;

        [SerializeField]
        private DroneTargetZone targetZone;

        [Header("State Machine")]

        /// <summary>Reference to the DroneStateManager script.</summary>
        [SerializeField]
        [Tooltip("Reference to the DroneStateManager script.")]
        private DroneStateManager droneStateManager;

        private List<DroneTarget> droneTargets = new ();

        private DroneTarget curDroneTarget;

        private Transform lastTargetPosition;

        private float positionProgress;

        private bool coroutineActive = false;

        private void Start()
        {
            droneTargets = targetZone.droneTargets;
            speedSave = droneSpeed;
            droneSpeed = startSpeed;
            Debug.Log(droneSpeed);
        }

        private void Update()
        {
            if (droneStateManager.droneStatus == DroneState.Patrol && !coroutineActive)
            {
                lastTargetPosition = transform;
                curDroneTarget = droneTargets[Random.Range(0, droneTargets.Count - 1)];
                StartCoroutine(this.Patrol());
                coroutineActive = true;
            }
            else if (droneStateManager.droneStatus == DroneState.Attack)
            {
                StopCoroutine(this.Patrol());
                coroutineActive = false;
            }
        }

        private void SetCurDroneTarget()
        {
            DroneTarget[] potentialDroneTargets = curDroneTarget.neighbourTargets;
            curDroneTarget = potentialDroneTargets[Random.Range(0, potentialDroneTargets.Length)];

            lastTargetPosition = transform;
            positionProgress = 0;
        }

        private void CheckCurDroneTargetReached()
        {
            if (Vector3.Distance(transform.position, curDroneTarget.transform.position) < distanceToTargetCompletion)
            {
                SetCurDroneTarget();
                droneSpeed = speedSave;
            }
        }

        private IEnumerator Patrol()
        {
            while (droneStateManager.droneStatus == DroneState.Patrol)
            {
                positionProgress += Time.deltaTime * droneSpeed;
                transform.position = Vector3.Lerp(lastTargetPosition.position, curDroneTarget.transform.position, positionProgress);
                //transform.forward = (curDroneTarget.transform.position - transform.position);

                Quaternion rotation = Quaternion.LookRotation(this.curDroneTarget.transform.position - this.transform.position);
                transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * this.rotationSpeed);

                CheckCurDroneTargetReached();

                yield return null;
            }
        }
    }
}