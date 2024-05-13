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






    //so in player collision use trygetcomponent returns true or false and pops out ref to that component

    public void IncreaseScore(float value)
    {
        _currentScore = _currentScore + value;
 
        return;
    }


    public void ReduceScore()
    {
        _currentScore /= _reduceScoreValue;
        return;
    }
 


}
