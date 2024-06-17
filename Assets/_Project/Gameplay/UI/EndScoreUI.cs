using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EndScoreUI : MonoBehaviour
{
    [SerializeField, Tooltip("How many seconds the UI will wait before displaying the score.")]
    private float _waitTime = 3;

    [Space]

    [SerializeField, Tooltip("The TextMesh that will display the player's final score on the end screen.")]
    private TextMeshProUGUI _endScoreDisplay;

    [SerializeField, Tooltip("The TextMesh that will display the player's grade on the end screen.")]
    private TextMeshProUGUI _gradeDisplay;

    [SerializeField, Tooltip("The game's Score System, holds the cash amount that will go into Score Display.")]
    private ScoreSystem _playerScore;

    //Enables score display and prints it to the player on the end screen.
    public void ShowResults()
    {
        _endScoreDisplay.enabled = true;

        if (_playerScore.CurrentScore <= 0)
            _endScoreDisplay.text = "$0";
        else
            _endScoreDisplay.text = "$" + _playerScore.CurrentScore;
    }

    //Gets the player's grade based on their end score
    public void ScoreGrade()
    {
        if (_playerScore.CurrentScore >= 1000f)
            _gradeDisplay.text = "Grade: " + "S";
        else if (_playerScore.CurrentScore < 1000f && _playerScore.CurrentScore > 750f)
            _gradeDisplay.text = "Grade: " + "A";
        else if (_playerScore.CurrentScore < 750f && _playerScore.CurrentScore > 250f)
            _gradeDisplay.text = "Grade: " + "B";
        else if (_playerScore.CurrentScore < 250f && _playerScore.CurrentScore > 0f)
            _gradeDisplay.text = "Grade: " + "C";
        else if (_playerScore.CurrentScore == 0f)
            _gradeDisplay.text = "Grade: " + "Cowabummer!";
        else if (_playerScore.CurrentScore >= 2500f)
            _gradeDisplay.text = "Grade: " + "Radical!";
    }
}
