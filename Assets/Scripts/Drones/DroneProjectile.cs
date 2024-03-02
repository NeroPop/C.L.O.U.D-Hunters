// <copyright file="DroneProjectile.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Drone.Attack
{
    using Player.Health;
    using Player.Stats;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>Class for drone projectile.</summary>
    public class DroneProjectile : MonoBehaviour
    {
        [Header("Projectile Values")]

        /// <summary>Damage of the projectile upon hitting the player.</summary>
        [SerializeField]
        [Tooltip("Damage of the projectile upon hitting the player.")]
        private float projectileDamage = 1.0f;

        /// <summary>Speed of the projectile.</summary>
        [SerializeField]
        [Tooltip("Speed of the projectile.")]
        private float projectileSpeed = 1.0f;

        [Header("Events")]

        /// <summary>Event called upon the projectile hitting a GameObject with the "Player" tag.</summary>
        [SerializeField]
        [Tooltip("Event called upon the projectile hitting a GameObject with the \"Player\" tag.")]
        private UnityEvent onPlayerHitEvent = new () { };

        /// <summary>Event called upon the projectile hitting a GameObject without the \"Player\" tag.</summary>
        [SerializeField]
        [Tooltip("Event called upon the projectile hitting a GameObject without the \"Player\" tag.")]
        private UnityEvent onObjectHitEvent = new () { };

        /// <summary>Direction of the projectile.</summary>
        private Vector3 direction;

        /// <summary>Gets or sets the target position of the projectile.</summary>
        public Vector3 TargetPosition { get; set; }

        /// <summary>Destroys this instance of the projectile.</summary>
        public void DestroyProjectile()
        {
            Destroy(this.gameObject);
        }

        private void Update()
        {
            if (this.direction == Vector3.zero)
            {
                this.direction = (this.TargetPosition - this.transform.position).normalized;
            }

            this.transform.position += this.projectileSpeed * Time.deltaTime * this.direction;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StatsManager.Instance.Stats["bullets received"] += 1.0f;
                HealthManager.Instance.ChangeHealth(-projectileDamage);
                this.onPlayerHitEvent.Invoke();
            }
            else if (collision.gameObject.CompareTag("Shield"))
            {
                StatsManager.Instance.Stats["bullets blocked"] += 1.0f;
                this.onObjectHitEvent.Invoke();
            }
            else
            {
                this.onObjectHitEvent.Invoke();
            }
        }
    }
}