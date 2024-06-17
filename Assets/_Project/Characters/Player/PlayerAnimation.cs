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
        float speed = _playerInput.MaxSpeed / 6;

        Vector3 maxVelocity = _body.velocity.normalized * speed;

        float currentSpeedByMax = _body.velocity.magnitude / maxVelocity.magnitude;

        currentSpeedByMax = Mathf.Clamp(currentSpeedByMax, 0.01f, maxVelocity.magnitude);

        _animator.SetFloat("Speed", currentSpeedByMax);
    }

    public void PlaySlip(float duration)
    {
        _animator.SetBool("Slipping", true);

        Invoke("StopSlip", duration);
    }

    private void StopSlip()
    {
        _animator.SetBool("Slipping", false);
    }

}
