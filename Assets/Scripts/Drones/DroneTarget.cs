// <copyright file="DroneTarget.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Drone.Patrol
{
    using UnityEngine;

    public class DroneTarget : MonoBehaviour
    {
        public DroneTarget[] neighbourTargets;

        [Header("Script References")]

        [SerializeField]
        private DroneTargetZone targetZone;

        private void Start()
        {
            targetZone.droneTargets.Add(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));

            if (neighbourTargets.Length > 0)
            {
                foreach (var target in neighbourTargets)
                {
                    Gizmos.DrawLine(transform.position, target.transform.position);
                }
            }
        }
    }
}