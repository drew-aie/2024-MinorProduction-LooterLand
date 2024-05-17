using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    //stores the effect that will be applied to the player.
    private Effect _effect;

    //property for _effect.
    public Effect CollectableEffect
    {
        get { return _effect; }

        set { _effect = value; }
    }

    //stores true if this effect has been applied.
    private bool _active;

    //used to change the particles the player emmits.
    private ParticleHandler _particles;

    //set _active to false.
    private void Awake()
    {
        _active = false;

        //gets the particlehandler component from the player.
        _particles = GetComponent<ParticleHandler>();
    }

    private void Update()
    {
        //if the effect exists and it is not active.
        if (_effect && !_active)
        {
            //apply the effect.
            _effect.Apply();

            //set active to be true.
            _active = true;
        }

        //if the effect exists and time is up.
        if (_effect && _effect.TimeLeft <= 0.1f)
        {
            //call the finish function of the effect.
            _effect.Finish();

            //get rid of the effect.
            _effect = null;

            //set active to false.
            _active = false;
        }

        if (_effect)
        {
            //change the particles of the player to have the material this effect stores.
            _particles.ChangeParticles(_effect.ParticleMaterial);
        }
        else
        {
            //set the current particle material to null.
            _particles.ChangeParticles(null);
        }
    }
}
