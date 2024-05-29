using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    [SerializeField, Tooltip("The Player's model.")]
    private GameObject _playerModel;

    //true if the player is actively blinking.
    private bool _currentlyBlinking = false;

    //flips from true to false and vice versa
    //for setting the player's active status to simulate blinking.
    private bool _renderActive;

    [SerializeField, Tooltip("The amount of blinks within the duration of the effect.")]
    private int _blinks;

    //the duration of the blinking effect.
    private float _duration;

    //sets the duration and begins the blinking coroutine.
    public void Activate(float duration)
    {
        //if the player is already blinking, dont activate again. 
        if (_currentlyBlinking)
            return;

        //sets the duration of this effect.
        _duration = duration;

        //set currently blinking to true.
        _currentlyBlinking = true;

        //true because the render is currently active.
        _renderActive = true;

        //begins the coroutine to blink the player.
        StartCoroutine("Blink");
    }

    //changes _renderActive to the opposite value.
    private void Flip()
    {
        _renderActive = !_renderActive;
    }

    //Coroutine 
    private IEnumerator Blink()
    {
        //toggles the player's model on and off.
        for (int i = 0; i < (_blinks * 2); i++)
        {
            //if the player model exists
            if (_playerModel)
            {
                //flip the value of _renderActive
                Flip();

                //sets the playermodel to the value of _renderActive.
                _playerModel.SetActive(_renderActive);
            }

            //waits for a fraction of the duration before this coroutine continues.
            yield return new WaitForSeconds(_duration/(_blinks * 2));
        }

        //if the player model exists, sets it to true.
        if (_playerModel)
            _playerModel.SetActive(true);

        //sets the player is blinking status to false.
        _currentlyBlinking = false;
    }
}
