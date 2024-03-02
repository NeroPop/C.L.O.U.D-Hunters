// <copyright file="CompanionOrbit.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Companionkjfn
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>CompanionOrbit builds a circle of points for the companion drone to move around.</summary>
    public class CompanionOrbit : MonoBehaviour
    {
        [Header("Circle")]

        /// <summary>Centre point of the orbit.</summary>
        [SerializeField]
        private Transform centrePointTransform;

        /// <summary>Prefab for the GameObject to instantiate at each point around the orbit.</summary>
        [SerializeField]
        private GameObject outsidePointGameObject;

        /// <summary>Radius of the orbit.</summary>
        [SerializeField]
        private float radius = 1.0f;

        /// <summary>Number of points around the orbit.</summary>
        [SerializeField]
        private int points = 45;

        /// <summary>List of all GameObject points.</summary>
        private GameObject[] circlePoints;

        /// <summary>Gets or sets circlePoints.</summary>
        public GameObject[] CirclePoints { get; set; }

        private void Awake()
        {
            this.CirclePoints = new GameObject[this.points];

            float angle = (float)Mathf.PI * 2 / (float)this.points;

            for (int i = 0; i < this.points; i++)
            {
                float x = this.radius * Mathf.Cos(angle * (i + 1.0f));
                float y = this.radius * Mathf.Sin(angle * (i + 1.0f));

                this.CirclePoints[i] = Instantiate(this.outsidePointGameObject, new Vector3(this.centrePointTransform.position.x + x, this.centrePointTransform.position.y, this.centrePointTransform.position.z + y), Quaternion.identity, this.centrePointTransform);
            }
        }
    }
}
