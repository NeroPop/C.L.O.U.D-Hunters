// <copyright file="DronePool.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Drones
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary><c>DronePool</c> uses object pooling to optimise drone instantiation/destruction.</summary>
    public class DronePool : MonoBehaviour
    {
        /// <summary>List of drones. The drone pool.</summary>
        private readonly List<GameObject> dronePool = new ();

        [Header("GameObject References")]

        /// <summary>Prefab for the drone. Used when there are no more available drones in the pool to take.</summary>
        [SerializeField]
        [Tooltip("Prefab for the drone. Used when there are no more available drones in the pool to take.")]
        private GameObject dronePrefab;

        /// <summary>Gets or sets the instance of the drone pool.</summary>
        public static DronePool Instance { get; set; }

        /// <summary>Deactivates a given drone and adds it to the drone pool.</summary>
        /// <param name="drone">The drone GameObject to add to the drone pool.</param>
        public void AddDroneToPool(GameObject drone)
        {
            drone.SetActive(false);
            drone.transform.parent = this.gameObject.transform;
            this.dronePool.Add(drone);
        }

        /// <summary>Returns a drone from the drone pool.</summary>
        /// <returns>Drone GameObject.</returns>
        public GameObject GetDroneFromPool()
        {
            GameObject drone;

            if (this.dronePool.Count == 0)
            {
                // if no drones are ready for use within the object pool, instantiate a new one
                drone = Instantiate(this.dronePrefab, null);
            }
            else
            {
                drone = this.dronePool[0];
                drone.transform.parent = null;
                drone.SetActive(true);

                this.dronePool.Remove(drone);
            }

            return drone;
        }

        /// <summary>Initiates singleton instance on awake.</summary>
        private void Awake()
        {
            Instance = this;
        }

        /*
         * TODO: Check if each child of this GameObject is a drone on runtime. If so, add it to the dronePool list. If not, remove it as a child.
        /// <summary>Initialises the drone pool.</summary>
        private void Start()
        {
            this.InitiateDronePool();
        }

        /// <summary>Initialises the drone pool on start. Adds any drones that are a child of this drone pool GameObject to the drone pool.</summary>
        private void InitiateDronePool()
        {
            foreach (GameObject child in this.gameObject.transform)
            {
                if (child.GetComponent<>()) // TODO: Add drone script reference to this line.
                {
                    // if the child is a drone, add it to the drone pool on play
                    child.SetActive(false);
                    this.dronePool.Add(child);
                }
                else
                {
                    // if the child is not a drone, remove the drone pool as it's parent.
                    child.transform.parent = null;
                }
            }
        }
         */
    }
}
