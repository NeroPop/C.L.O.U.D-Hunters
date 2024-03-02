// <copyright file="DroneTargetZone.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Drone.Patrol
{
    using System.Collections.Generic;
    using UnityEngine;

    public class DroneTargetZone : MonoBehaviour
    {
        [HideInInspector]
        public List<DroneTarget> droneTargets = new ();
    }
}