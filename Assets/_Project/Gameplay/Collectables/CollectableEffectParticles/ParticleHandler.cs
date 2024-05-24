using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    //holds the particle system of this gameObject.
    private ParticleSystem _particles;

    private void Awake()
    {
        //gets a reference to the player's particle system.
        _particles = GetComponent<ParticleSystem>();
    }

    public void ChangeMaterial(Material material)
    {
        //changes the particle material.
        _particles.GetComponent<ParticleSystemRenderer>().material = material;
    }
}
