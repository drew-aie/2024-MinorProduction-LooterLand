using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SpeedBoost : Effect
{
    //gets the Input component.
    private Input _playerMovement;

    //the default speed of the player.
    private float _originalSpeed;

    [SerializeField, Tooltip("The modifier that will boost player speed.")]
    private float _speedModifier;

    //true if this effect is active.
    private bool _active;

    //property for _active. [Read-Only]
    public bool Active
    {
        get { return _active; }
    }

    public void Awake()
    {
        //get the Input component.
        _playerMovement = gameObject.GetComponent<Input>();

        //stores the original speed of the player.
        _originalSpeed = _playerMovement.MaxSpeed;

        //stores false by default.
        _active = false;
    }

    public override void Apply()
    {
        //modifies the speed of the player.
        _playerMovement.MaxSpeed *= _speedModifier;

        //sets active to true.
        _active = true;

        //set the timer and invoke event.
        base.Apply();
    }

    public override void Finish()
    {
        //returns the player speed to the original value.
        _playerMovement.MaxSpeed = _originalSpeed;

        //sets active to false.
        _active = false;

        //invokes event.
        base.Finish();
    }
}
