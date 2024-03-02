// <copyright file="HealthUIBase.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Player.Health
{
    using UnityEngine;

    public class HealthUIBase : MonoBehaviour
    {
        public virtual void Start()
        {
            HealthManager.Instance.healthUIElements.Add(this);
        }

        public virtual void UpdateUI(float curHealth, float maxHealth)
        {

        }
    }
}