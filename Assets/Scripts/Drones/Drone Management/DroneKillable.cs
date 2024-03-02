// <copyright file="DroneKillable.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Drones
{
    using Player.Stats;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// <c>DroneKillable</c> class handles killing drones by detecting when the disc or other drone killing objects collide with it.
    /// </summary>
    public class DroneKillable : MonoBehaviour
    {
        /// <summary>The parent drone GameObject.</summary>
        [SerializeField]
        [Tooltip("The parent drone GameObject.")]
        private GameObject droneParent;

        /// <summary>
        /// Event invoked upon a drone being killed.
        /// </summary>
        [SerializeField]
        [Tooltip("Event invoked upon a drone being killed.")]
        private UnityEvent onDroneKill;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Disk") || other.gameObject.CompareTag("FingerGod"))
            {
                StatsManager.Instance.Stats["drones destroyed"] += 1;

                this.onDroneKill.Invoke();

                DronePool.Instance.AddDroneToPool(this.droneParent);
            }
        }
    }
}
