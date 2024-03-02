// <copyright file="VolumeSetter.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Menu
{
    using FMODUnity;
    using UnityEngine;

    /// <summary><c>VolumeSetter</c> is a helper class for setting customisable variables via inspector events.</summary>
    public class VolumeSetter : MonoBehaviour
    {
        [Header("Script References")]

        /// <summary>Script Reference for <c>StudioGlobalParameterTrigger</c> associated to the appropriate volume setting.</summary>
        [SerializeField]
        [Tooltip("Script Reference for StudioGlobalParameterTrigger associated to the appropriate volume setting.")]
        private StudioGlobalParameterTrigger studioGlobalParameterTriggerScript;

        /// <summary>Sets the volume (mapped between 0 and 1) on user input.</summary>
        /// <param name="step">User input.</param>
        public void SetVolume(int step)
        {
            float stepMap = (100-step) / 100.0f;

            this.studioGlobalParameterTriggerScript.Value = stepMap;
        }
    }
}
