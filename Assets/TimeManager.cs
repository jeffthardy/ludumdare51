using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public bool timerEnabled;

    private float timerTime;



    // Start is called before the first frame update
    void Start()
    {
        timerEnabled = false;
        StartTimer();

    }

    // Update is called once per frame
    void Update()
    {
        if (timerEnabled)
        {
            timerTime += Time.deltaTime;
            if(timerTime >= 10.0)
            {
                // Trigger 10s effects here.
                timerTime = 0;
                TimerFinished?.Invoke();
            }
        }

        //timerText.text = ((float)((int)(timerTime * 100)) / 100.0f).ToString("0.00");
        timerText.text = ((int)(timerTime)).ToString();
    }


    public void StartTimer()
    {
        timerEnabled = true;
        timerTime = 0;
        TimerStarted?.Invoke();

    }
    public void StopTimer()
    {
        timerEnabled = false;

    }


    public float GetTimer()
    {
        return timerTime;
    }


    public delegate void TimeFinishedDelegate();
    public event TimeFinishedDelegate TimerFinished;

    public delegate void TimeStartedDelegate();
    public event TimeStartedDelegate TimerStarted;
}
