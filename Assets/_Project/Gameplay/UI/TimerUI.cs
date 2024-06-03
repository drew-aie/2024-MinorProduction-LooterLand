using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerUI : MonoBehaviour
{
    [SerializeField, Tooltip("The Textmesh that will display the timer countdown to the player.")]
    private TextMeshProUGUI _timerDisplay;

    [SerializeField, Tooltip("The display for the Time's Up prompt.")]
    private TextMeshProUGUI _timesUpDisplay;

    [Space]

    [Tooltip("The length of the countdown in minutes")]
    [SerializeField, Min(0)]
    private int _minutes = 1;

    [Tooltip("The length of the countdown in seconds")]
    [SerializeField, Min(0)]
    private int _seconds = 0;

    private float _countDown;

    private void TimesUp()
    {
        //Disabling and enabling timer and time's up display
        _timerDisplay.enabled = false;
        _timesUpDisplay.enabled = true;

        //Vector3 vec = new Vector3(1.0f, 0.0f, 0.0f);
        //_timesUpDisplay.transform.DOPunchPosition(vec, 2);
    }

    void Start()
    {
        //Guard clause that caps seconds at 60
        if (_seconds >= 60)
            _seconds = 60;

        //Multiplies minutes value by 60 and adds the seconds value to it
        _countDown = (60 * _minutes) + _seconds;
    }

    // Update is called once per frame
    void Update()
    {
        //Guard clause that won't run update without a timer display
        if (!_timerDisplay)
            return;

        //Countdown decrements with delta time each update call if above 0
        if (_countDown > 0.0f)
            _countDown -= Time.deltaTime;

        //Converts countdown value into minutes and seconds
        int minutes = Mathf.FloorToInt(_countDown / 60);
        int seconds = Mathf.FloorToInt(_countDown % 60);

        //Displays the minutes and seconds in the timer ui
        _timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (_countDown <= 0.0f)
            TimesUp();
    }
}
