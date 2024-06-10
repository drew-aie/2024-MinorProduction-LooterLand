using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField, Tooltip("The animator component which animates this Cop.")]
    private Animator _animator;

    private Rigidbody _body;

    private Input _playerInput;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();

        _playerInput = GetComponent<Input>();
    }

    private void Update()
    {
        float speed = _playerInput.MaxSpeed / 3;

        Vector3 maxVelocity = _body.velocity.normalized * speed;

        float currentSpeedByMax = _body.velocity.magnitude / maxVelocity.magnitude;

        currentSpeedByMax = Mathf.Clamp(currentSpeedByMax, 0.1f, maxVelocity.magnitude);

        _animator.SetFloat("Speed", currentSpeedByMax);
    }

}
