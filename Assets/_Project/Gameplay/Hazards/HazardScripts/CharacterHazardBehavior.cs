using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterHazardBehavior : MonoBehaviour
{
    //the default physics effect applied to this gameObject.
    private PhysicMaterial _default;

    //the rigidbody of this character.
    private Rigidbody _body;

    private float _elapsedTime;

    private float _duration;

    private bool _effectActive;

    private bool _isSlippery;

    private Vector3 _slipDirection;

    [SerializeField, Tooltip("The script that is responsible for playing water droplet particles.")]
    private Liquid_Effect _characterDropletParticleSystems;

    [SerializeField, Tooltip("Scales the force applied for character movement when slipping.")]
    private float _slipSupressionScalar;

    private void Awake()
    {
        _elapsedTime = 0;

        _effectActive = false;

        //stores the gameObject's default physics material.
        _default = GetComponent<CapsuleCollider>().material;

        _body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_elapsedTime > _duration)
            //sets _effectActive to false.
            DefaultEffect();

        if (_effectActive)
        {
            _elapsedTime += Time.fixedDeltaTime;

            if (_isSlippery && gameObject.CompareTag("Player"))
                //apply the last velocity had before becoming slippery
                _body.AddForce(_slipDirection * Time.fixedDeltaTime);
        }
    }

    public void ChangePhysics(PhysicMaterial newPhysics, float duration, bool isSlippery, float slipStrengthMagnifier, Material waterMaterial)
    {
        _elapsedTime = 0;

        _duration = duration;

        _effectActive = true;

        _isSlippery = isSlippery;

        //get the characters current velocity, magnified.
        _slipDirection = _body.velocity.normalized * slipStrengthMagnifier * 10000 /*offset*/;

        if (_isSlippery)
        {
            if (gameObject.TryGetComponent(out Input playerInput))
            {
                playerInput.SuppressInput(_slipSupressionScalar, duration);

                gameObject.GetComponent<PlayerAnimation>().PlaySlip(duration);
            }
            else if (gameObject.TryGetComponent(out CopSeekBehavior copBehavior))
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;

                copBehavior.SuppressInput(_slipSupressionScalar, duration);

                gameObject.GetComponent<CopAnimation>().PlaySlip(duration);
            }
        }
        else
            if(TryGetComponent(out NavMeshAgent nav))
            {
                nav.enabled = false;
            }

        _characterDropletParticleSystems.Play(waterMaterial);

        GetComponent<CapsuleCollider>().material = newPhysics;

    }

    public void DefaultEffect()
    {
        _elapsedTime = 0;

        _duration = 0;

        _effectActive = false;

        _isSlippery = false;

        GetComponent<CapsuleCollider>().material = _default;

        _characterDropletParticleSystems.Stop();

        if (TryGetComponent(out NavMeshAgent nav))
        {
            nav.enabled = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
