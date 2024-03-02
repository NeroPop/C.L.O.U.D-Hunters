// <copyright file="DroneAttack.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Drone.Attack
{
    using System.Collections;
    using Drones.Management;
    using Drone.StateMachine;
    using UnityEngine;

    /// <summary><c>DroneAttack</c> creates the viewcone and handles the drone attacking state.</summary>
    public class DroneAttack : MonoBehaviour
    {
        [Header("Viewcone Settings")]

        /// <summary>Distance of the viewcone.</summary>
        [SerializeField]
        [Tooltip("Distance of the viewcone.")]
        private float distance = 5.0f;

        /// <summary>Angle of the viewcone.</summary>
        [SerializeField]
        [Range(1, 360)]
        [Tooltip("Angle of the viewcone.")]
        private float angle = 30.0f;

        /// <summary>Height of the viewcone.</summary>
        [SerializeField]
        [Tooltip("Height of the viewcone.")]
        private float height = 3f;

        /// <summary>Viewcone GameObject.</summary>
        [SerializeField]
        [Tooltip("Viewcone GameObject.")]
        private GameObject viewcone;

        [Header("Player Detection Settings")]

        /// <summary>Number of times to scan for the player per second.</summary>
        [SerializeField]
        [Tooltip("Number of times to scan for the player per second.")]
        private int scansPerSecond = 1;

        /// <summary>Transform the drone casts a ray from when trying to determine line of sight with the player.</summary>
        [SerializeField]
        [Tooltip("Transform the drone casts a ray from when trying to determine line of sight with the player.")]
        private Transform viewport;

        [SerializeField]
        private GameObject[] playerTargets;

        [Header("Attack Settings")]

        /// <summary>How many shots to fire per second.</summary>
        [SerializeField]
        [Tooltip("How many shots to fire per second.")]
        private int shotsPerSecond;

        /// <summary>Speed that the drone rotates to view the player.</summary>
        [SerializeField]
        [Tooltip("Speed that the drone rotates to view the player.")]
        private float rotationSpeed = 1.0f;

        [SerializeField]
        [Tooltip("")]
        private float moveToDistance = 4.0f;

        [SerializeField]
        [Tooltip("")]
        private float moveToSpeed = 1.0f;

        /// <summary>Reference to the projectile prefab.</summary>
        [SerializeField]
        [Tooltip("Reference to the projectile prefab.")]
        private GameObject projectilePrefab;

        /// <summary>Reference the the gunbarrel GameObject.</summary>
        [SerializeField]
        [Tooltip("Reference the the gunbarrel GameObject.")]
        private GameObject gunbarrel;

        [Header("State Machine")]

        /// <summary>Reference to the DroneStateManager script.</summary>
        [SerializeField]
        [Tooltip("Reference to the DroneStateManager script.")]
        private DroneStateManager droneStateManager;

        // attack variables

        /// <summary>Target GameObject.</summary>
        private GameObject target = null;

        /// <summary>Timer for managing the number of shots per second.</summary>
        private float shotTimer;

        /// <summary>Interval between shots.</summary>
        private float shotInterval;

        // player scan variables

        /// <summary>Timer for managing the number of scans per second.</summary>
        private float scanTimer;

        /// <summary>Interval between scans.</summary>
        private float scanInterval;

        private void Start()
        {
            this.shotInterval = 1.0f / this.shotsPerSecond;
            this.scanInterval = 1.0f / this.scansPerSecond;
        }

        private void Update()
        {
            this.ShotTimer();
            this.ScanTimer();
        }

        /// <summary>Handles the drone shot timer.</summary>
        private void ShotTimer()
        {
            this.shotTimer -= Time.deltaTime;

            if (this.shotTimer < 0)
            {
                this.shotTimer += this.shotInterval;
                this.Shoot();
            }
        }

        /// <summary>Coroutine that rotates the drone towards the player.</summary>
        /// <returns><c>null</c>.</returns>
        private IEnumerator RotateToPlayer()
        {
            while (this.target != null)
            {
                Quaternion rotation = Quaternion.LookRotation(this.target.transform.position - this.transform.position);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * this.rotationSpeed);
                yield return null;
            }
        }

        private IEnumerator MoveToPlayer()
        {
            while (this.target != null)
            {
                float distance = Vector3.Distance(this.transform.position, this.target.transform.position);

                if (distance > moveToDistance)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, this.target.transform.position, Time.deltaTime * moveToSpeed);
                }

                yield return null;
            }
        }

        /// <summary>Shoots a drone projectile.</summary>
        private void Shoot()
        {
            if (this.target != null && this.droneStateManager.droneStatus == DroneState.Attack)
            {
                GameObject projectile = Instantiate(this.projectilePrefab, this.gunbarrel.transform.position, this.gunbarrel.transform.rotation);
                DroneProjectile projectileComponent = projectile.GetComponent<DroneProjectile>();
                projectileComponent.TargetPosition = this.target.transform.position;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                this.target = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                this.target = null;
            }
        }

        /// <summary>Handles the drone scan timer.</summary>
        private void ScanTimer()
        {
            this.scanTimer -= Time.deltaTime;

            if (this.scanTimer < 0)
            {
                this.scanTimer += this.scanInterval;
                this.Scan();
            }
        }

        /// <summary>Scans for line of sight with the player, if the player is within this drone's viewcone.</summary>
        private void Scan()
        {
            if (this.target)
            {
                foreach (GameObject playerTarget in playerTargets)
                {
                    if (Physics.Raycast(this.viewport.position, playerTarget.transform.position - this.viewport.position, out RaycastHit hit, (LayerMask.GetMask("Disk", "Bullet", "IgnoreRaycast", "Hand"))))
                    {
                        if (hit.collider.gameObject.CompareTag("Player"))
                        {
                            this.droneStateManager.droneStatus = DroneState.Attack;
                            this.transform.parent.GetComponent<DroneCombatPool>().CombatStart();
                            this.StartCoroutine(this.RotateToPlayer());
                            this.StartCoroutine(this.MoveToPlayer());
                            return;
                        }
                    }
                }
            }

            this.StopCoroutine(this.RotateToPlayer());
            this.StopCoroutine(this.MoveToPlayer());
            this.droneStateManager.droneStatus = DroneState.Patrol;
        }

        /// <summary>Creates the viewcone mesh.</summary>
        /// <returns>The viewcone mesh.</returns>
        private Mesh CreateMesh()
        {
            Mesh mesh = new ();

            int segments = 10;
            int numTriangles = (segments * 4) + 2 + 2;
            int numVertices = numTriangles * 3;

            Vector3[] vertices = new Vector3[numVertices];
            int[] triangles = new int[numVertices];

            Vector3 bottomCenter = Vector3.zero;
            Vector3 bottomLeft = Quaternion.Euler(0, -this.angle, 0) * Vector3.forward * this.distance;
            Vector3 bottomRight = Quaternion.Euler(0, this.angle, 0) * Vector3.forward * this.distance;

            Vector3 topCenter = bottomCenter + (Vector3.up * this.height);
            Vector3 topLeft = bottomLeft + (Vector3.up * this.height);
            Vector3 topRight = bottomRight + (Vector3.up * this.height);

            int vert = 0;

            // left side
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomLeft;
            vertices[vert++] = topLeft;

            vertices[vert++] = topLeft;
            vertices[vert++] = topCenter;
            vertices[vert++] = bottomCenter;

            // right side
            vertices[vert++] = bottomCenter;
            vertices[vert++] = topCenter;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomCenter;

            float currentAngle = -this.angle;
            float deltaAngle = (this.angle * 2) / segments;
            for (int i = 0; i < segments; i++)
            {
                bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * this.distance;
                bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * this.distance;

                topRight = bottomRight + (Vector3.up * this.height);
                topLeft = bottomLeft + (Vector3.up * this.height);

                // far edge
                vertices[vert++] = bottomLeft;
                vertices[vert++] = bottomRight;
                vertices[vert++] = topRight;

                vertices[vert++] = topRight;
                vertices[vert++] = topLeft;
                vertices[vert++] = bottomLeft;

                // top
                vertices[vert++] = topCenter;
                vertices[vert++] = topLeft;
                vertices[vert++] = topRight;

                // bottom
                vertices[vert++] = bottomCenter;
                vertices[vert++] = bottomRight;
                vertices[vert++] = bottomLeft;

                currentAngle += deltaAngle;
            }

            for (int i = 0; i < numVertices; i++)
            {
                triangles[i] = i;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }

        /// <summary>Creates the viewcone.</summary>
        private void CreateViewcone()
        {
            MeshCollider viewconeMesh = this.viewcone.GetComponent<MeshCollider>();
            viewconeMesh.sharedMesh = this.CreateMesh();
        }

        private void OnValidate()
        {
            this.CreateViewcone();
        }
    }
}
