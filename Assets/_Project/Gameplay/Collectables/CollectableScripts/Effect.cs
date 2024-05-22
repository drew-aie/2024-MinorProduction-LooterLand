using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    //event that occurs when an effect begins.
    private UnityEvent OnEffectStart;

    //event that occurs when an effect finishes.
    private UnityEvent OnEffectFinish;


    private void Start()
    {
        //give the duration to the time left.
        _timeLeft = _duration;
    }

    //applies the effect.
    public virtual void Apply()
    {
        //give the duration to the time left when we apply the effect.
        _timeLeft = _duration;

        //invokes that should occur on the start of this effect.
        OnEffectStart.Invoke();
    }

    private void Update()
    {
        //reduces time left by deltaTime.
        _timeLeft -= Time.deltaTime;
    }

    //undoes the effect.
    public virtual void Finish()
    {
        //invokes that should occur on the end of this effect.
        OnEffectFinish.Invoke();
    }
}
