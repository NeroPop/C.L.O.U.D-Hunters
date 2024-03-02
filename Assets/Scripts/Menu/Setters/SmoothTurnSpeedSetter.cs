// <copyright file="SmoothTurnSpeedSetter.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Menu
{
    using HexabodyVR.PlayerController;
    using UnityEngine;

    /// <summary><c>SmoothTurnSpeedSetter</c> is a helper class for setting customisable variables via inspector events.</summary>
    public class SmoothTurnSpeedSetter : MonoBehaviour
    {
        [Header("XR Rigs")]

        /// <summary>Oculus Rig.</summary>
        [SerializeField]
        [Tooltip("Oculus Rig.")]
        private GameObject oculusRig;

        /// <summary>OpenXR Rig.</summary>
        [SerializeField]
        [Tooltip("OpenXR Rig.")]
        private GameObject openXRRig;

        [Header("Script References")]

        /// <summary>Reference to the Oculus Rig's <c>HexaBodyPlayer4</c> script.</summary>
        [SerializeField]
        [Tooltip("Reference to the Oculus Rig's HexaBodyPlayer4 script.")]
        private HexaBodyPlayer4 hexaBodyPlayer4Oculus;

        /// <summary>Reference to the OpenXR Rig's <c>HexaBodyPlayer4</c> script.</summary>
        [SerializeField]
        [Tooltip("Reference to the OpenXR Rig's HexaBodyPlayer4 script.")]
        private HexaBodyPlayer4 hexaBodyPlayer4OpenXR;

        /// <summary>Cache of the <c>HexaBodyPlayer4</c> used in this build.</summary>
        private HexaBodyPlayer4 curHexaBodyPlayer4;

        private void Start()
        {
            if (this.oculusRig.activeInHierarchy)
            {
                this.curHexaBodyPlayer4 = hexaBodyPlayer4Oculus;
            }
            else if (this.openXRRig.activeInHierarchy)
            {
                this.curHexaBodyPlayer4 = hexaBodyPlayer4OpenXR;
            }
        }

        /// <summary>Sets the smooth turn speed based on user input.</summary>
        /// <param name="step">User input.</param>
        public void SetSmoothTurnSpeed(int step)
        {
            this.curHexaBodyPlayer4.SmoothTurnSpeed = 350 -step;
        }
    }
}
