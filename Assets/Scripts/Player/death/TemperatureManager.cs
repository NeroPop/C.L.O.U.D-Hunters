using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TemperatureManager : MonoBehaviour
{

    private float trueTemperature = 36.5f; // True native temperature you wish the player to bet set at
    public float currentTemperature; // the actual temperature the player is set at

    [SerializeField] public float maxTemperature; // the maxixum temperature the player can reach
    [SerializeField] public float minTemperature; // threshold at which player starts taking damage/dies
    [SerializeField] private float passiveMaxTemperature;

    public float changeTempCold;
    public float changeTempHot;


    public float coldTickFrequency;
    public float hotTickFrequency; //Time passed when heating up, should be shorter than originalTimer as this is for heating up.


    private float timer = 5f; //Sets the timer length 
    private bool warmingUP; //Bool for whether we are in a hotzone or not
    private bool tooHot; // bool for whether the heat is too hot
    public bool freezing;


    public Text txt;

    [SerializeField] private Image _warmUpArrow;

    [SerializeField] private Image _coolDownArrow;

    [SerializeField] private AudioSource _warningSound;

    [SerializeField] private GameObject _warningLight;

    public GameObject newDeathSystem;
    // public bool isFrozen; 

    // public bool returningForward;
    // public bool returningPause;
    //public bool returningBackward;


    public void Start()
    {
        warmingUP = false; // Sets the players hotzone to false to start player taking damage, change this whenever you want the passive damage to change for the player
        tooHot = false; //The player will not spawn on fire
        freezing = false;


        currentTemperature = trueTemperature; //Sets the current temperature to the native temperature since this is script should be used when player is spawning in, unless changed.

        _warmUpArrow.GetComponent<Image>().enabled = false;
        _coolDownArrow.GetComponent<Image>().enabled = false;
        _warningSound.enabled = false;
        _warningLight = GameObject.Find("Warning Light");
        _warningLight.active = false;

        //newDeathSystem = GameObject.Find("Death Manger");

    }

    public void Update()
    {

        if (timer <= 0.0f && warmingUP == false && freezing == true)
        {
            Debug.Log("im freezing");
            decreaseTemperaturex2();
        }

        if (timer <= 0.0f && warmingUP == false && freezing == false) // If timer has executed while player is outside of a hot zone, they will start to implement damage
        {
            decreaseTemperature();
        }




        if (timer <= 0.0f && warmingUP == true) // if timer has exectued while player is inside a hotzone do, do this 
        {

            increaseTemperature();
        }

        if (currentTemperature <= minTemperature)
        {
            newDeathSystem.GetComponent<newDeathSystem>().Death(); //DEATH
            currentTemperature = trueTemperature;
        }

        if (currentTemperature <= 29)
        {
            _warningSound.enabled = true;
            Debug.Log("audioplaying");

            _warningLight.active = true;
            Debug.Log("LightOn");
        }

        if (currentTemperature > 29)
        {
            _warningSound.enabled = false;

            _warningLight.active = false;

        }



        // txt.text = currentTemperature.ToString();
        txt.text = string.Format("{0:#.00}", currentTemperature);

    }



    /// <summary>
    /// Decreases temperature over by x
    /// </summary>
    public void decreaseTemperature()
    {

        timer = coldTickFrequency;
        currentTemperature -= changeTempCold;
        _warmUpArrow.GetComponent<Image>().enabled = false;
        _coolDownArrow.GetComponent<Image>().enabled = true;


    }
    /// <summary>
    /// Increases temperature by x
    /// </summary>
    public void increaseTemperature()
    {

        if ((currentTemperature + changeTempHot) >= passiveMaxTemperature)
        {
            Debug.Log("Im Maxed");
            currentTemperature = passiveMaxTemperature;
            currentTemperature -= changeTempHot; // This operation is required due toa bug where the currentTemperature is bigger than wanted  by an offset of the difference between the changing temperature.
                                                 // Tried to put in a calculation to prevent it from going overboard


        }

        _warmUpArrow.GetComponent<Image>().enabled = true;
        _coolDownArrow.GetComponent<Image>().enabled = false;

        timer = hotTickFrequency;
        currentTemperature += changeTempHot;



    }

    public void decreaseTemperaturex2()
    {

        timer = 3.5f;
        currentTemperature -= changeTempCold;

        _warmUpArrow.GetComponent<Image>().enabled = false;
        _coolDownArrow.GetComponent<Image>().enabled = true;


    }


    /// <summary>
    /// Use this to induce true and false statements for hotZones as colliders
    /// </summary>
    public void induceWarmingTrue()
    {
        warmingUP = true;
    }

    public void induceWarmingFalse()
    {
        warmingUP = false;
    }

    public void freezeFalse()
    {
        freezing = false;
    }
    public void freeze()
    {
        freezing = true;
    }

    /// <summary>
    /// Moves time forwards
    /// </summary>
    public void moveTimeDown()
    {
        timer -= Time.deltaTime;

    }



    public void moveTimeUp()
    {
        timer += Time.deltaTime;
    }

    public void FixedUpdate()
    {

        moveTimeDown();
        Debug.Log(currentTemperature);
    }
}
