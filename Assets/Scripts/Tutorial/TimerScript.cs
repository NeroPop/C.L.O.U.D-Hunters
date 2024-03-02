using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class TimerScript : MonoBehaviour
{
    public float TimeLimit;
    private float TimeLeft = 15;
    private bool TimerOn = false;
    private bool TimeStop = false;

    [SerializeField] private TMP_Text TimerText;

    [SerializeField] private UnityEvent Pass;
    [SerializeField] private UnityEvent Fail;
 
    public void StartTimer()
    {
        TimeLeft = TimeLimit;
        TimerOn = true;
        TimeStop = false;
    }

    public void StopTimer()
    {
        TimeStop = true;
        TimerOn = false;
        Result();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if(TimeLeft > 0 && !TimeStop)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else if (TimeLeft <= 0)
            {
                TimerOn = false;
                Result();
            }
        }

        else if (TimeStop)
        {
            TimerOn = false;
            Result();
        }

        else
        {
            TimeLeft = 0;
            TimerOn = false;
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private void Result()
    {
        if (TimeLeft > 0)
        {
            Pass.Invoke();
        }
        else
        {
            Fail.Invoke();
        }
    }
}
