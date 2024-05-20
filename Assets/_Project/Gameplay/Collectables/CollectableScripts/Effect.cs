using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField, Tooltip("The total time before this effect is disabled.")]
    private float _duration;
    //property for _duration. [Read-Only]
    public float Duration
    {
        get { return _duration; }
    }
    //the time left before this effect ends.
    private float _timeLeft;
    //property for _timeLeft. [Read-Only]
    public float TimeLeft
    {
        get { return _timeLeft; }
    }

    private void Start()
    {
        //give the duration to the time left before finish is called.
        _timeLeft = _duration;
    }

    //applies the effect.
    public virtual void Apply()
    {

    }

    private void Update()
    {
        //reduces time left by deltaTime.
        _timeLeft -= Time.deltaTime;
    }

    //undoes the effect.
    public virtual void Finish()
    {

    }
}
