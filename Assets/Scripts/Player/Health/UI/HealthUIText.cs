// <copyright file="HealthUIText.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Player.Health
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthUIText : HealthUIBase
    {
        [Header("GameObject References")]

        [SerializeField]
        [Tooltip("")]
        private TMP_Text textElement;

        public override void Start()
        {
            base.Start();
            textElement.text = HealthManager.Instance.GetHealth().ToString();
        }

        public override void UpdateUI(float curHealth, float maxHealth)
        {
            textElement.text = curHealth.ToString();
        }
    }
}