// <copyright file="DroneCombatPool.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Drones.Management
{
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>DroneCombatPool manages instances of individual combat.</summary>
    public class DroneCombatPool : MonoBehaviour
    {
        /// <summary>Event called upon the player killing all of the drones in this instance of combat.</summary>
        [SerializeField]
        [Tooltip("Event called upon the player killing all of the drones in this instance of combat.")]
        private UnityEvent onCombatPoolEmpty = new ();

        [SerializeField] private UnityEvent OnCombatStart = new();
        private bool combatStarted = false;

        /// <summary>Cache of if the onCombatPoolEmpty event has triggered or not. Prevents the onCombatPoolEmpty event being called multiple times after all drones have died.</summary>
        private bool isTriggered = false;

        private void Update()
        {
            // when a drone dies using the global drone pool, it changes their parent GameObject to the global drone pool
            // therefore we can use the childCount of our smaller "combat pool" to tell if all the drones have "died" or not
            if (this.gameObject.transform.childCount > 0 && isTriggered)
            {
                isTriggered = false;
            }
            else if (this.gameObject.transform.childCount == 0 && !this.isTriggered)
            {
                this.onCombatPoolEmpty.Invoke();
            }
        }

        public void CombatStart()
        {
            if (!combatStarted)
            {
                combatStarted = true;
                this.OnCombatStart.Invoke();
            }
        }
    }
}