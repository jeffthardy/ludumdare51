using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicClip;
    public TimeManager timeManager;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        timeManager.TimerStarted += TimerStartedHandler;
        timeManager.TimerFinished += TimerFinishedHandler;
        audioSource = GetComponent<AudioSource>();
        if(timeManager.timerEnabled)
        {
            // timer started in start for timeManager so we could miss it depending on order
            TimerStartedHandler();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void TimerStartedHandler()
    {
        Debug.Log("Starting Music!");
        audioSource.clip = musicClip;
        audioSource.Play();
    }

    // Catch if we finished our song but the level timer is still going... might want to end level here if songs determine end
    private void TimerFinishedHandler()
    {
        if (timeManager.timerEnabled)
        {
            if (audioSource.isPlaying == false)
                TimerStartedHandler();
        }
    }
}
