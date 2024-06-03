using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimesUp : MonoBehaviour
{
    [SerializeField, Tooltip("The display for the countdown.")]
    private GameObject _timerDisplay;

    [SerializeField, Tooltip("The display for when the timer reaches zero.")]
    private GameObject _timesUp;

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
    }
}
