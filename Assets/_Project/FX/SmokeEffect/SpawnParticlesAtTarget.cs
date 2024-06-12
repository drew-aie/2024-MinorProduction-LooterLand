using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticlesAtTarget : MonoBehaviour
{
    [SerializeField, Tooltip("The target gameObject to spawn particles at.")]
    private GameObject _target;

    [SerializeField, Tooltip("The Particles to spawn at the target location.")]
    private ParticleSystem _particles;

    private void Awake()
    {
        _particles.Stop();
    }

    //moves the particle system to the target and plays the effect.
    public void Invoke()
    {
        _particles.Stop();

        gameObject.transform.position = _target.transform.position;

        _particles.Play();
    }
}
