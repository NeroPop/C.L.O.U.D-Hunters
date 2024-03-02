using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Player.Stats;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class DiskHealthIncreaser : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    [SerializeField] private float healthIncrease; // Amount of health increased on each bounce.
    [Range(0.0f, 5.0f)]
    [SerializeField] private float ShieldIncrease;

    private int wallHits; // Number of times the wall is hit.
    private int droneHits; // Number of drones that have been hit. Dunno what this is gonna be used for.

    private int healthIncreases = 0;
    private int shieldIncreases = 0;

    public bool inHand;
    [SerializeField] private bool grab;
    [SerializeField] private bool debugging = false;
    [SerializeField] private Player.Shield.Shield shield;

    [SerializeField] private UnityEvent DiskBounce;
    [SerializeField] private UnityEvent onAudioPlay;
    [SerializeField] private float audioInterval;
    //private int audioRepeats;
    private float timer;

    private void Start()
    {
        wallHits = 0;
        droneHits = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (debugging == true)
        {
            if (grab == true && inHand != true)
            {
                inHand = true;
                OnDiskGrab();
            }
        }

        if (healthIncreases > 0 && timer >= audioInterval)
        {
            IncreaseHealth();
            healthIncreases--;
            timer = 0.0f;
        }

        if (shieldIncreases > 0 && timer >= audioInterval)
        {
            IncreaseShields();
            shieldIncreases--;
            timer = 0.0f;
        }
        

        if (grab == false)
        {
            inHand = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(inHand != true)
        {

            if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Hand"))
            {
                wallHits++;
                DiskBounce.Invoke();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inHand != true)
        {
            if (other.gameObject.tag == "Drone")
            {
                droneHits++;
            }
        }
    }

    /// <summary>
    /// When the disk is grabbed, 
    /// </summary>
    public void OnDiskGrab()
    {
        inHand = true;

        healthIncreases = wallHits;
        shieldIncreases = droneHits;

        /*float totalHealthIncrease = wallHits * healthIncrease;

        float currentHealth = Player.Health.HealthManager.Instance.GetHealth();
        float maxHealth = Player.Health.HealthManager.Instance.GetMaxHealth();

        currentHealth += totalHealthIncrease;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Player.Health.HealthManager.Instance.SetHealth(currentHealth);
        shield.AddCharge(droneHits * ShieldIncrease);*/

        StatsManager.Instance.Stats["disk bounces"] += wallHits;

        wallHits = 0;
        droneHits = 0;
    }
   
    public void IncreaseHealth()
    {
        float currentHealth = Player.Health.HealthManager.Instance.GetHealth();
        float maxHealth = Player.Health.HealthManager.Instance.GetMaxHealth();

        if (currentHealth >= maxHealth)
            return;

        currentHealth += healthIncrease;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        Player.Health.HealthManager.Instance.SetHealth(currentHealth);
        onAudioPlay.Invoke();
    }

    public void IncreaseShields()
    {
        shield.AddCharge(ShieldIncrease);
    }
}
