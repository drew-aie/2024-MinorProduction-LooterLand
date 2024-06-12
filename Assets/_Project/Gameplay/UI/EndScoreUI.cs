using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScoreUI : MonoBehaviour
{
    [SerializeField, Tooltip("How many seconds the UI will wait before displaying the score.")]
    private float _waitTime = 3;

    [Space]

    [SerializeField, Tooltip("The TextMesh that will display the player's final score on the end screen.")]
    private TextMeshProUGUI _endScoreDisplay;

    [SerializeField, Tooltip("The game's Score System, holds the cash amount that will go into Score Display.")]
    private ScoreSystem _playerScore;

    public void ShowResults()
    {
        _endScoreDisplay.enabled = true;

        if (_playerScore.CurrentScore <= 0)
            _endScoreDisplay.text = "$0";
        else
            _endScoreDisplay.text = "$" + _playerScore.CurrentScore;
    }
}
