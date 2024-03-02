// <copyright file="StatGetter.cs" company="Lucky8">
// Copyright (c) Lucky8. All rights reserved.
// </copyright>

namespace Player.Stats
{
    using TMPro;
    using UnityEngine;

    /// <summary>Gets the specified stat and sets it to a text component.</summary>
    public class StatGetter : MonoBehaviour
    {
        [SerializeField]
        private string statID;

        [SerializeField]
        private TMP_Text textComponent;

        private void OnEnable()
        {
            this.GetStat();
        }

        private void GetStat()
        {
            if (StatsManager.Instance.Stats.ContainsKey(this.statID))
            {
                // Override for displaying time based stats
                if (this.statID == "time played" || this.statID == "longest life")
                {
                    string minutes = ((int)(StatsManager.Instance.Stats[this.statID] / 60)).ToString();
                    string seconds;

                    if ((int)(StatsManager.Instance.Stats[this.statID] % 60) < 10)
                    {
                        seconds = ("0" + (int)(StatsManager.Instance.Stats[this.statID] % 60));
                    }
                    else
                    {
                        seconds = ((int)(StatsManager.Instance.Stats[this.statID] % 60)).ToString();
                    }

                    this.textComponent.text = (minutes + ":" + seconds);
                    return;
                }

                this.textComponent.text = ((int)StatsManager.Instance.Stats[this.statID]).ToString();
            }
            else
            {
                this.textComponent.text = "0";
            }
        }
    }
}
