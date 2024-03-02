// <copyright file="EQParameterTrigger.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace FMOD.Equaliser
{
    using FMODUnity;
    using UnityEngine;

    /// <summary><c>EQParameterTrigger</c> sets the value of a StudioParameterTrigger variable based on the y position of a GameObject.</summary>
    public class EQParameterTrigger : MonoBehaviour
    {
        private enum Axis
        {
            x,
            y,
            z,
        }

        [Header("Value Settings")]

        /// <summary>FMOD Parameter GameObject with the desired variable to change.</summary>
        [SerializeField]
        [Tooltip("FMOD Parameter GameObject with the desired variable to change.")]
        private GameObject ParameterGameObject;

        /// <summary>Name of the variable to change.</summary>
        [SerializeField]
        [Tooltip("Name of the variable to change. Case sensitive.")]
        private string variableName;

        /// <summary>Minimum value to map to.</summary>
        [SerializeField]
        [Tooltip("Minimum value to map to.")]
        private float minValue;

        /// <summary>Maximum value to map to.</summary>
        [SerializeField]
        [Tooltip("Maximum value to map to.")]
        private float maxValue;

        /// <summary>Should the value ascend or descend as the GameObject ascends?</summary>
        [SerializeField]
        [Tooltip("Should the value ascend or descend as the GameObject ascends?")]
        private bool invertAxis;

        /// <summary>Should the value revert back to <c>defaultValue</c> when the GameObject leaves the trigger?</summary>
        [SerializeField]
        [Tooltip("Should the value revert back to Default Value when the GameObject leaves the trigger?")]
        private bool revertToDefault = false;

        /// <summary>Default value to revert to upon the GameObject leaving the trigger.</summary>
        [SerializeField]
        [Tooltip("Default value to revert to upon the GameObject leaving the trigger.")]
        private float defaultValue;

        [Header("Reference GameObject Settings")]

        /// <summary>Oculus GameObject to measure the height of.</summary>
        [SerializeField]
        [Tooltip("Oculus GameObject to measure the height of.")]
        private GameObject oculusReferenceGameObject;

        /// <summary>OpenXR GameObject to measure the height of.</summary>
        [SerializeField]
        [Tooltip("OpenXR GameObject to measure the height of.")]
        private GameObject openXRReferenceGameObject;

        /// <summary>The axis to measure the Reference GameObject on.</summary>
        [SerializeField]
        [Tooltip("The axis to measure the Reference GameObject on.")]
        private Axis axis = Axis.y;

        /// <summary>Minimum height value.</summary>
        [SerializeField]
        [Tooltip("Minimum height value.")]
        private float minDistance;

        /// <summary>Maximum height value.</summary>
        [SerializeField]
        [Tooltip("Maximum height value.")]
        private float maxDistance;

        [Header("Script References")]

        /// <summary><c>StudioParameterEmitter</c> script with the associated variable being changed.</summary>
        [SerializeField]
        [Tooltip("StudioParameterEmitter script with the associated variable being changed.")]
        private StudioEventEmitter studioEventEmitter;

        /// <summary>Cache of the maximum value minus minimum value.</summary>
        private float valueRange;

        /// <summary>Cache of the maximum height minus minimum height.</summary>
        private float distanceRange;

        /// <summary>If the player is in the trigger or not.</summary>
        private bool inTrigger;

        /// <summary>GameObject to measure the height of.</summary>
        private GameObject referenceGameObject;

        private void Start()
        {
            this.valueRange = this.maxValue - this.minValue;
            this.distanceRange = this.maxDistance - this.minDistance;

            if (oculusReferenceGameObject.activeInHierarchy)
            {
                referenceGameObject = oculusReferenceGameObject;
            }
            else if (openXRReferenceGameObject.activeInHierarchy)
            {
                referenceGameObject = openXRReferenceGameObject;
            }
        }

        private void Update()
        {
            if (this.inTrigger)
            {
                this.SetVal(this.CalculateVal());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == this.referenceGameObject)
            {
                this.inTrigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == this.referenceGameObject)
            {
                this.inTrigger = false;
            }

            if (this.revertToDefault)
            {
                this.SetVal(defaultValue);
            }
        }

        /// <summary>Calculates the value of the variable based on the GameObject position.</summary>
        private float CalculateVal()
        {
            float position = 0.0f;

            if (axis == Axis.x)
            {
                position = this.referenceGameObject.transform.position.x;
            }
            else if (axis == Axis.y)
            {
                position = this.referenceGameObject.transform.position.y;
            }
            else if (axis == Axis.z)
            {
                position = this.referenceGameObject.transform.position.z;
            }

            if (position <= this.minDistance)
            {
                return this.minValue;
            }
            else if (position >= this.maxDistance)
            {
                return this.maxValue;
            }

            float gameObjectDistance = position - this.minDistance;

            if (this.invertAxis)
            {
                return -((this.valueRange * (gameObjectDistance / (1 - this.distanceRange))) + this.minValue);
            }

            return (this.valueRange * (gameObjectDistance / this.distanceRange)) + this.minValue;
        }

        /// <summary>Sets the value.</summary>
        private void SetVal(float value)
        {
            studioEventEmitter.EventInstance.setParameterByName(variableName, value);
        }
    }
}