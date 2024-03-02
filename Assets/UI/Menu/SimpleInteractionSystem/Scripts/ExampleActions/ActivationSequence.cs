using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationSequence : MonoBehaviour
{
    bool isBusy = false;

    public GameObject[] objects;
    public float delayTime=0.25f;
    public float disableAfterSeconds = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

    public void StartSequence()
    {
        if (isBusy == false)
            StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        isBusy = true;

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
            StartCoroutine(DisableObject(objects[i]));
            yield return new WaitForSeconds(delayTime);
        }
        isBusy = false;
        yield return null;
    }

    IEnumerator DisableObject(GameObject g)
    {
        yield return new WaitForSeconds(disableAfterSeconds);
        g.SetActive(false);
    }
}
