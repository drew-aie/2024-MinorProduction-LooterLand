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

    [SerializeField, Tooltip("The panel holding the timer and it's background image")]
    private GameObject _timerUI;

    [Space]

    [Tooltip("The length of the countdown in minutes")]
    [SerializeField, Min(0)]
    private int _minutes = 1;

    [Tooltip("The length of the countdown in seconds")]
    [SerializeField, Min(0)]
    private int _seconds = 0;

    [Tooltip("When the timer will begin to shake to warn the player that time is almost up.")]
    [SerializeField, Min(0)]
    private float _warningTime = 20f;

    private float _countdown;
    private Vector3 _test = new Vector3(0.32f, 0.275f, 0f);

    //Property for timer countdown. [Read-Only]
    public float Countdown => _countdown;

    void Start()
    {
        //Guard clause that caps seconds at 60
        if (_seconds >= 60)
            _seconds = 60;

        //Multiplies minutes value by 60 and adds the seconds value to it
        _countdown = (60 * _minutes) + _seconds;
    }

    // Update is called once per frame
    void Update()
    {
        //Guard clause that won't run update without a timer display
        if (!_timerDisplay || !_timerUI)
            return;

        //Countdown decrements with delta time each update call if above 0
        if (_countdown > 0.0f)
            _countdown -= Time.deltaTime;

        //Converts countdown value into minutes and seconds
        int minutes = Mathf.FloorToInt(_countdown / 60);
        int seconds = Mathf.FloorToInt(_countdown % 60);

        //Displays the minutes and seconds in the timer ui
        _timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //Shaking timer if countdown is less than warning time
        if (_countdown <= _warningTime)
            HurryUp();

        if (_countdown <= 0.0f)
        {
            _timerDisplay.enabled = false;
            _timerUI.SetActive(false);
        }
    }

    //Functionality for when the player is almost out of time
    private void HurryUp()
    {
        //Making timer red
        _timerDisplay.DOColor(Color.red, 1f);
        //Shaking timer canvas
        _timerUI.transform.DOShakePosition(1f, _test, 10, 75, false, true, ShakeRandomnessMode.Harmonic);
    }
}
