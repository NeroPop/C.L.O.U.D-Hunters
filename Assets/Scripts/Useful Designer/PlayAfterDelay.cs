
using UnityEngine;
using UnityEngine.Events;

public class PlayAfterDelay : MonoBehaviour
{
    public float delayTime = 1f;
    public UnityEvent delayedActions;

    bool isBusy = false;


    public void Play()
    {
        if (isBusy == false)
        {
            isBusy = true;
            Invoke("RunUnityEvent", delayTime);
            Invoke("ResetBusy", delayTime);
        }
    }

    public void PlayDelay(float newDelayTime)
    {
        if (isBusy == false)
        {
            isBusy = true;
            Invoke("RunUnityEvent", newDelayTime);
            Invoke("ResetBusy", newDelayTime);
        }
    }

    void RunUnityEvent()
    {
        delayedActions.Invoke();
    }

    void ResetBusy()
    {
        isBusy = false;
    }
}
