using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Player's Current Total Score")]
   private float _currentScore;

    public float CurrentScore
    { get { return _currentScore; } set { _currentScore = value; } }

    [SerializeField]
    [Tooltip("Value The Player's score is reduced by")]
    private float _reduceScoreValue = 2;

    //the magnitude we will reduce the score.
    public float ReduceScoreValue
    {
        get { return _reduceScoreValue; }
    }





    /// <summary>
    /// increases the current score value by whatever float is passed into this function.
    /// </summary>
    /// <param name="value">the amount being added to the current score</param>
    public void IncreaseScore(float value)
    {
        _currentScore = _currentScore + value;
        
        return;
    }

    /// <summary>
    /// divides the current score value by the reduce score value then rounds off at the 2 decimal points.
    /// </summary>
    public void ReduceScore()
    {
        _currentScore /= _reduceScoreValue;
        _currentScore = Mathf.Round(_currentScore * 100.0f) * 0.01f;
        return;
    }
 


}
