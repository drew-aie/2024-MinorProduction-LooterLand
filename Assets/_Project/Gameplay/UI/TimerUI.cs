using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerUI : MonoBehaviour
{
    [SerializeField, Tooltip("The Textmesh that will display the timer countdown to the player.")]
    private TextMeshProUGUI _timerDisplay;

    [Space]

    [Tooltip("The length of the countdown in minutes")]
    [SerializeField, Min(0)]
    private int _minutes = 1;

    [Tooltip("The length of the countdown in seconds")]
    [SerializeField, Min(0)]
    private int _seconds = 0;

    private float _countDown;

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

        //Countdown decrements with delta time each update call
        _countDown -= Time.deltaTime;

        //Converts countdown value into minutes and seconds
        int minutes = Mathf.FloorToInt(_countDown / 60);
        int seconds = Mathf.FloorToInt(_countDown % 60);

        //Displays the minutes and seconds in the timer ui
        _timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
