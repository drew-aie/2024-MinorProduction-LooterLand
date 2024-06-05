using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField, Tooltip("The Particle system this will modify.")]
    private ParticleSystem _particles;

    public void ChangeMaterial(Material material)
    {
        //changes the particle material.
        _particles.GetComponent<ParticleSystemRenderer>().material = material;
    }

    private void Update()
    {
        //lock rotation.
        _particles.gameObject.transform.rotation = Quaternion.identity;
    }
}
