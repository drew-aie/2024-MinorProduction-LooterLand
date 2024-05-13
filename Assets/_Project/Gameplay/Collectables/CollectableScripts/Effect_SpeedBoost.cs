using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SpeedBoost : Effect
{

    [SerializeField, Tooltip("The player that this effect will modify.")]
    private GameObject _player;


    [SerializeField, Tooltip("The material of the particles this effect will emmit.")]
    private Material _particleMaterial;

    [SerializeField, Tooltip("The data container that stores the time duration and speed modifier values.")]
    private Collectable_Effect_DataSO _effectData;

    //the speed that the player started with.
    private float _startingSpeed;

    //the Input component of this player. 
    //(used to modify the speed of the player)
    private Input _playerInput;

    //stores true if the speedboost effect has already been applied.
    private bool _applied;

    private void Awake()
    {
        //sets the remaining time of this effect. 
        TimeLeft = _effectData.EndTime;

        //get the player's Input component.
        _playerInput = _player.GetComponent<Input>();

        //stores the original speed of the player.
        _startingSpeed = _playerInput.Speed;

        //stores the set particle material.
        ParticleMaterial = _particleMaterial;
    }
    public override void Apply()
    {
        if(!_applied)
        {
            //apply the speedboost effect.
            _playerInput.Speed *= _effectData.SpeedMagnifier;

            //set _applied to true.
            _applied = true;
        }

        //call the base function.
        base.Apply();
        
    }

    //called upon the end of this effect.
    public override void Finish()
    {
        //returns the Player's speed to default. 
        _playerInput.Speed = _startingSpeed;

        //call the base function.
        base.Finish();
    }
}
