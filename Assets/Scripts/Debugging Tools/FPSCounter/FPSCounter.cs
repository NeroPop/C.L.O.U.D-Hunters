using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    const float TICK = 1.0f;
    private float timer = 0.0f;

    // Text Objects
    [SerializeField] private TMP_Text counterText; // Normal counter text object
    [SerializeField] private TMP_Text lCounterText; // Lower counter text object
    //[SerializeField] private TMP_Text aCounterText;
    [SerializeField] private TMP_Text hCounterText; // Higher counter text object

    // Frame Counts
    private int countThisFrame = 0;
    private int lowestCount = 240;
    private int highestCount = 0;
    //private int averageFPS = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Count timer
        countThisFrame++; // Increase frames by 1

        if(timer > TICK)
        {
            counterText.text = countThisFrame.ToString(); // Change the current frame count text.

            // If frames drop lower than the current lowest frame count, change text to reflect.
            if(countThisFrame < lowestCount)
            {
                lowestCount = countThisFrame;
                lCounterText.text = lowestCount.ToString();
            }

            // If frames grow higher than the current highest frame count, change text to reflect.
            if (countThisFrame > highestCount)
            {
                highestCount = countThisFrame;
                hCounterText.text = highestCount.ToString();
            }

            countThisFrame = 0;
            timer = 0.0f;
        }
    }
}
