// <copyright file="StatsManager.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Player.Stats
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>Manages the storing of player stats.</summary>
    public class StatsManager : MonoBehaviour
    {
        /// <summary>Dictionary containing stats.</summary>
        [HideInInspector]
        public Dictionary<string, float> Stats = new ()
        {
            { "drones destroyed", 0.0f },
            { "disk bounces", 0.0f },
            { "deaths", 0.0f },
            { "longest life", 0.0f },
            { "health lost", 0.0f },
            { "health gained", 0.0f },
            { "time played", 0.0f },
            { "bullets received", 0.0f },
            { "bullets blocked", 0.0f },
        };

        /// <summary>Gets or sets the singleton instance of <c>StatsManager</c>.</summary>
        public static StatsManager Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            UpdateTimePlayed();
        }

        private void UpdateTimePlayed()
        {
            Stats["time played"] = Time.realtimeSinceStartup;
        }
    }
}
