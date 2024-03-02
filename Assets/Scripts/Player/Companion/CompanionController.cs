// <copyright file="CompanionController.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Companionkjfn
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>Companion Controller controls the movement of the companion sphere.</summary>
    public class CompanionController : MonoBehaviour
    {
        /// <summary>Reference to CompanionOrbit script.</summary>
        [SerializeField]
        [Tooltip("Reference to CompanionOrbit script.")]
        private CompanionOrbit companionOrbit;

        /// <summary>Transform for the companion drone to look at.</summary>
        [SerializeField]
        [Tooltip("Transform for the companion drone to look at.")]
        private Transform lookAtTransform;

        [Header("Companion Variables.")]

        /// <summary>Speed of the companion drone.</summary>
        [SerializeField]
        [Tooltip("Speed of the companion drone.")]
        private float speed = 0.8f;

        /// <summary>Minimum amount of time before finding a new point in orbit to go to.</summary>
        [SerializeField]
        [Tooltip("Minimum amount of time before finding a new point in orbit to go to.")]
        private float minTimeForNewPosition = 4.0f;

        /// <summary>Maximum amount of time before finding a new point in orbit to go to.<zsummary>
        [SerializeField]
        [Tooltip("Maximum amount of time before finding a new point in orbit to go to.")]
        private float maxTimeForNewPosition = 4.0f;

        /// <summary>Target position that the drone is trying to move to.</summary>
        private Vector3 targetPosition;

        private void Start()
        {
            this.StartCoroutine(this.FindNextPoint());
            this.StartCoroutine(this.Move());
            this.transform.position = this.targetPosition;
        }

        private void Update()
        {
            Debug.Log(this.targetPosition);
        }

        private IEnumerator Move()
        {
            while (true)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, this.targetPosition, this.speed * Time.deltaTime);

                Quaternion rotation = Quaternion.LookRotation(this.lookAtTransform.position - this.transform.position);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, this.speed * Time.deltaTime);

                yield return null;
            }
        }

        /// <summary>Coroutine to find a new point in the players orbit that the companion drone can move to.</summary>
        private IEnumerator FindNextPoint()
        {
            while (true)
            {
                List<GameObject> availablePoints = this.companionOrbit.CirclePoints.ToList();
                GameObject nextPoint;

                while (availablePoints.Count > 0)
                {
                    int random = Random.Range(0, this.companionOrbit.CirclePoints.Length);
                    nextPoint = this.companionOrbit.CirclePoints[random];

                    if (Physics.OverlapSphere(nextPoint.transform.position, 0.5f).Length == 0)
                    {
                        this.targetPosition = nextPoint.transform.position;
                        break;
                    }
                    else
                    {
                        availablePoints.Remove(nextPoint);
                    }
                }

                yield return new WaitForSeconds(Random.Range(this.minTimeForNewPosition, this.maxTimeForNewPosition));
            }
        }
    }
}
