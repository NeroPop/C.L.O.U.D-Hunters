// <copyright file="HealthManager.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Player.Health
{
    using System.Collections.Generic;
    using Player.Stats;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary><c>HealthManager</c> handles the player's health.</summary>
    public class HealthManager : MonoBehaviour
    {
        [Header("Player Health Variables")]

        /// <summary>Minimum health of the player.</summary>
        [SerializeField]
        [Tooltip("Minimum health of the player.")]
        private float minHealth = 0f;

        /// <summary>Maximum health of the player.</summary>
        [SerializeField]
        [Tooltip("Maximum health of the player.")]
        private float maxHealth = 100f;

        /// <summary>Current health of the player. Also the starting health of the player.</summary>
        [SerializeField]
        [Tooltip("Current health of the player. Also the starting health of the player.")]
        private float curHealth = 100f;

        /// <summary>Point at which the player will be notified about low health.</summary>
        [SerializeField]
        [Tooltip("event at which player reaches low health")]
        private float lowHealth = 10f;

        /// <summary>Check to make sure the onsafe health event only happens once.</summary>
        private bool safeHealth = true;

        [Header("Events")]

        /// <summary>Invokes associated methods upon the player gaining health.</summary>
        [SerializeField]
        [Tooltip("Invokes associated methods upon the player gaining health.")]
        private UnityEvent onHealthGained = new() { };

        /// <summary>Invokes associated methods upon the player taking damage.</summary>
        [SerializeField]
        [Tooltip("Invokes associated methods upon the player taking damage.")]
        private UnityEvent onDamageTaken = new() { };

        /// <summary>Invokes associated methods upon player death.</summary>
        [SerializeField]
        [Tooltip("Invokes associated methods upon player death.")]
        private UnityEvent onDeath = new() { };

        /// <summary>Invokes associated methods upon player reaching low health.</summary>
        [SerializeField]
        [Tooltip("Invokes associated methods upon player reaching low health.")]
        private UnityEvent onLowHealth = new() { };

        /// <summary>Invokes associated methods one time upon player escaping low health.</summary>
        [SerializeField]
        [Tooltip("Invokes associated methods one time upon player escaping low health.")]
        private UnityEvent onsafeHealth = new() { };

        /// <summary>Gets or sets the singleton instance of <c>HealthManager</c>.</summary>
        public static HealthManager Instance { get; set; }

        public List<HealthUIBase> healthUIElements = new() { };

        /// <summary>Initiates singleton instance on awake.</summary>
        private void Awake()
        {
            Instance = this;
        }

        /// <summary>Returns the health of the player as a raw float.</summary>
        /// <returns>Health of the player.</returns>
        public float GetHealth()
        {
            return this.curHealth;
        }

        /// <summary>Returns the health of the player as a float mapped between 0 and 1.</summary>
        /// <returns>Health of the player as a percentage.</returns>
        public float GetHealthPercentage()
        {
            return this.curHealth / this.maxHealth;
        }

        /// <summary>Gets the maximum health of the player.</summary>
        /// <returns>Maximum health of the player.</returns>
        public float GetMaxHealth()
        {
            return this.maxHealth;
        }

        /// <summary>Sets the health of the player. Use <c>ChangeHealth()</c> to change the player's health by a particular amount.</summary>
        /// <param name="amount">Amount to set the player's health to.</param>
        public void SetHealth(float amount)
        {
            float healthBefore = this.curHealth;

            if (amount == this.curHealth)
            {
                return;
            }
            else if (amount < this.curHealth)
            {
                this.onDamageTaken.Invoke();
            }
            else if (amount > this.curHealth)
            {
                this.onHealthGained.Invoke();
            }
            
            this.curHealth = amount;
            this.UpdateUIElements();
            this.CheckHealth(healthBefore);
        }

        /// <summary>Sets the maximum health of the player.</summary>
        /// <param name="amount">Amount to set the player's maximum health to.</param>
        public void SetMaxHealth(float amount)
        {
            if (amount == this.maxHealth)
            {
                return;
            }

            this.maxHealth = amount;
            this.UpdateUIElements();
            this.CheckHealth(0.0f);
        }

        /// <summary>Changes the health of the player by parameter <c>amount</c>.</summary>
        /// <param name="amount">Amount of health to change.</param>
        public void ChangeHealth(float amount)
        {
            float healthBefore = this.curHealth;

            if (amount == 0)
            {
                return;
            }
            else if (amount < 0)
            {
                this.onDamageTaken.Invoke();
            }
            else if (amount > 0)
            {
                this.onHealthGained.Invoke();
            }

            this.curHealth += amount;
            this.UpdateUIElements();
            this.CheckHealth(healthBefore);
        }

        private void UpdateUIElements()
        {
            foreach (HealthUIBase healthUI in healthUIElements)
            {
                healthUI.UpdateUI(curHealth, maxHealth);
            }
        }

        /// <summary>Invokes <c>onDeath</c> if health is below minimum value, sets <c>curHealth</c> to maximum health if the player's health is above maximum value.</summary>
        private void CheckHealth(float healthBefore)
        {
            if (this.curHealth <= this.minHealth)
            {
                this.onDeath.Invoke();
            }
            else if (this.curHealth <= this.lowHealth)
            {
                this.onLowHealth.Invoke();
                safeHealth = false;
            }
            else if (this.curHealth > this.lowHealth && !safeHealth)
            {
                this.onsafeHealth.Invoke();
                safeHealth = true;
            }
            else if (this.curHealth > this.maxHealth)
            {
                this.curHealth = this.maxHealth;
            }

            if (this.curHealth - healthBefore > 0.0f)
            {
                StatsManager.Instance.Stats["health gained"] = this.curHealth - healthBefore;
            }
            else if (this.curHealth - healthBefore < 0.0f)
            {
                StatsManager.Instance.Stats["health lost"] = -(this.curHealth - healthBefore);
            }
        }
    }
}
