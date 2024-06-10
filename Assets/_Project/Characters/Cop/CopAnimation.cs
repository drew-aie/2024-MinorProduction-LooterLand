using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class CopAnimation : MonoBehaviour
{
    [SerializeField, Tooltip("The animator component which animates this Cop.")]
    private Animator _animator;

    private NavMeshAgent _nav;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float speed = _nav.speed;

        Vector3 maxVelocity = _nav.velocity.normalized * speed;

        float currentSpeedByMax = _nav.velocity.magnitude / maxVelocity.magnitude;

        _animator.SetFloat("Speed", currentSpeedByMax);
    }

}
