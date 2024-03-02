// <copyright file="DroneStateManager.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Drone.StateMachine
{
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>Enum for representing the drone state machine.</summary>
    public enum DroneState
    {
        /// <summary>Attack state.</summary>
        Attack,

        /// <summary>Patrol state.</summary>
        Patrol,
    }

    /// <summary><c>DroneStateManager</c> handles the drone state machine.</summary>
    public class DroneStateManager : MonoBehaviour
    {
        [Header("Events")]

        /// <summary>Event invoked upon the drone starting. This will be when the drone is loaded in via scene loading.</summary>
        [SerializeField]
        [Tooltip("Event invoked upon the drone starting. This will be when the drone is loaded in via scene loading.")]
        private UnityEvent onDroneStart = new();

        /// <summary>Event invoked upone the drone entering attack state.</summary>
        [SerializeField]
        [Tooltip("Event invoked upone the drone entering attack state.")]
        private UnityEvent onAttackStateEnter = new ();

        /// <summary>Event invoked upon the drone entering patrol state.</summary>
        [SerializeField]
        [Tooltip("Event invoked upon the drone entering patrol state.")]
        private UnityEvent onPatrolStateEnter = new();

        /// <summary>Status of the drone state machine.</summary>
        [HideInInspector]
        public DroneState droneStatus = DroneState.Patrol;

        /// <summary>Cache of <c>droneStatus</c>. Used to invoke events upon entering new states.</summary>
        private DroneState curStatus = DroneState.Patrol;

        private void Start()
        {
            onDroneStart.Invoke();
        }

        private void Update()
        {
            if (curStatus != droneStatus)
            {
                if (droneStatus == DroneState.Attack)
                {
                    onAttackStateEnter.Invoke();
                }
                else if (droneStatus == DroneState.Patrol)
                {
                    onPatrolStateEnter.Invoke();
                }

                curStatus = droneStatus;
            }
        }
    }
}