// <copyright file="ShieldManager.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Player.Shield
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class Shield : MonoBehaviour
    {
        [Header("Shield Settings")]

        [SerializeField]
        private int maxCharge;

        [SerializeField]
        private float rechargeSpeed;

        [SerializeField]
        private float depleteSpeed;

        [Header("Sliders")]

        [SerializeField] 
        private Slider LeftOculusSlider;

        [SerializeField]
        private Slider RightOculusSlider;

        [SerializeField]
        private Slider LeftOpenXRSlider;

        [SerializeField]
        private Slider RightOpenXRSlider;

        [Header("Events")]

        [SerializeField]
        private UnityEvent onShieldOpenL;

        [SerializeField]
        private UnityEvent onShieldOpenR;

        [SerializeField]
        private UnityEvent onShieldClose;

        [SerializeField]
        private UnityEvent onChargeAdded;

        private bool shieldOpen = false;

        [Header("XR-Rigs")]

        [SerializeField]
        private GameObject Oculus;

        [SerializeField]
        private GameObject OpenXR;

        [Header("Debug")]

        [SerializeField]
        public bool LeftHand = false;

        public float curCharge;

        [SerializeField]
        private Slider LeftSlider;

        [SerializeField]
        private Slider RightSlider;

        private void Start()
        {
            curCharge = maxCharge;

            if (Oculus.activeInHierarchy)
            {
                LeftSlider = LeftOculusSlider;
                RightSlider = RightOculusSlider;
            }
            else
            {
                LeftSlider = LeftOpenXRSlider;
                RightSlider = RightOpenXRSlider;
            }
        }

        private void Update()
        {
            if (!shieldOpen)
            {
                RechargeShield();
            }
            else if (shieldOpen)
            {
                DepleteShield();
                LeftSlider.value = curCharge;
                RightSlider.value = curCharge;
            }

            CheckChargeBounds();
        }

        private void OpenShieldL()
        {
            if (LeftHand)
            {
                onShieldOpenL.Invoke();
                shieldOpen = true;
            }
        }
        private void OpenShieldR()
        {
            if (!LeftHand)
            {
                onShieldOpenR.Invoke();
                shieldOpen = true;
            }
        }

        private void CloseShield()
        {
            onShieldClose.Invoke();
            shieldOpen = false;
        }

        private void RechargeShield()
        {
            if (curCharge < maxCharge)
            {
                curCharge += Time.deltaTime * rechargeSpeed;
            }
        }

        private void DepleteShield()
        {
            curCharge -= Time.deltaTime * depleteSpeed;
            
            if (curCharge <= 0.0f)
            {
                CloseShield();
            }
        }

        private void CheckChargeBounds()
        {
            if (curCharge < 0.0f)
            {
                curCharge = 0.0f;
            }
            else if (curCharge > maxCharge)
            {
                curCharge = maxCharge;
            }
        }

        public void OpenShieldButtonL()
        {
            if (!shieldOpen && LeftHand)
            {
                OpenShieldL();
            }
        }

        public void OpenShieldButtonR()
        {
            if (!shieldOpen && !LeftHand)
            {
                OpenShieldR();
            }
        }

        public void CloseShieldButtonL()
        {
            if (shieldOpen && LeftHand)
            {
                CloseShield();
            }
        }

        public void CloseShieldButtonR()
        {
            if (shieldOpen && !LeftHand)
            {
                CloseShield();
            }
        }

        public void AddCharge(float amount)
        {
            curCharge += amount;
            onChargeAdded.Invoke();
        }

        public void LeftHanded()
        {
            LeftHand = true;
        }
        public void RightHanded()
        {
            LeftHand = false;
        }

    }
}