using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField, Tooltip("The TextMesh that will display the player's score in the UI.")]
    private TextMeshProUGUI _scoreDisplay;

    [SerializeField, Tooltip("The game's Score System, holds the cash amount that will go into Score Display.")]
    private ScoreSystem _playerScore;

    // Update is called once per frame
    void Update()
    {
        if (!_scoreDisplay || !_playerScore)
            return;

        //Casting score to be an int
        _playerScore.CurrentScore = (int)_playerScore.CurrentScore;
        //Inserting score plus and other quirks into the text mesh
        _scoreDisplay.text = "$" + _playerScore.CurrentScore + ".99";
        Debug.Log(_playerScore.CurrentScore);
    }
}
