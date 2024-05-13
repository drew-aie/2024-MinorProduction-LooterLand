using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    [TooltipAttribute("Player's Current Total Score")]
   private float _currentScore;

    public float CurrentScore
    { get { return _currentScore; } set { _currentScore = value; } }

    [SerializeField]
    [TooltipAttribute("Value The Player's score is redduced by")]
    private float _reduceScoreValue = 2;

    //The Collectable the player has collided with.
   private Collectable _collidedCollectable;



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
