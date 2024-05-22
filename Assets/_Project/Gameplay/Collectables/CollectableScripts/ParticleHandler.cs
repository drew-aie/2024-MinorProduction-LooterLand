using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    //stores a reference to the _particleRenderer component.
    private ParticleSystemRenderer _particleRenderer;

    //stores our particle renderer component.
    private void Awake()
    {
        _particleRenderer = GetComponent<ParticleSystemRenderer>();
    }

    //changes the particle material on our particles.
    public void ChangeParticles(Material mat)
    {
        _particleRenderer.material = mat;
    }
}
