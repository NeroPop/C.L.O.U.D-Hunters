using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreTracker : MonoBehaviour
{
    //Check and set time 
    private float timeElapsed = 0f;
    private bool isTiming = false;

    private void Start()
    {
        //start timer
        isTiming = true;
        StartCoroutine(UpdateTimer());
    }
    /// <summary>
    /// Simple IEnumator to keep track of time since start
    /// </summary>
    
    private IEnumerator UpdateTimer()
    {
        while (isTiming)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }


    //Here we set one of our win game conditionals. We also pass float timeTaken which will be our score for this game.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WayPoint"))
        {
            isTiming = false;
            float timeTaken = timeElapsed;
            GameManager.Instance.WinGame(timeTaken);
        }
    }
}
