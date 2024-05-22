using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private float _timeLeft;
    //property for _timeLeft
    public float TimeLeft
    {
        get { return _timeLeft; }
        set { _timeLeft = value; }
    }

    private Material _particleMaterial;

    //property for _particleMaterial.
    public Material ParticleMaterial
    {
        get => _particleMaterial;
        set => _particleMaterial = value;
    }

    //called to apply the effect to the player.
    public virtual void Apply()
    {
       
    }

    //reduces the time left.
    private void FixedUpdate()
    {
        _timeLeft = Time.fixedDeltaTime;
    }

    //called upon the end of this effect.
    public virtual void Finish()
    {

    }

}
