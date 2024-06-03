using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimesUp : MonoBehaviour
{
    [SerializeField, Tooltip("The display for the countdown.")]
    private GameObject _timerDisplay;

    [SerializeField, Tooltip("The display for when the timer reaches zero.")]
    private GameObject _timesUp;

    public UnityEvent OnCountdownEnd;

    // Update is called once per frame
    void Update()
    {
        //Guard Clause
        if (!_timerDisplay)
            return;

        //Storing countdown from timer script
        float countdown = _timerDisplay.GetComponent<TimerUI>().Countdown;

        if (countdown <= 0)
            TimeIsUp();
    }

    //Handles what the game should do after the timer reaches 0
    private void TimeIsUp()
    {
        //Activating times up display
        _timesUp.SetActive(true);
        //Shaking the display
        _timesUp.transform.DOShakePosition(1f);

        //Using coroutine to making having a buffer easier
        StartCoroutine(Buffer(OnCountdownEnd.Invoke, 3f));
    }

    //Standard IEnumerator delay
    private IEnumerator Buffer(Action callback, float delay)
    {
        yield return new WaitForSeconds(delay);
        callback();
    }
}
