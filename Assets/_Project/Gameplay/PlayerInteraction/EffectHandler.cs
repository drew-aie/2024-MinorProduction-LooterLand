using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    //stores the ID of the current effect.
    private EffectID _currentEffect;

    //stores the current effects attached to the player.
    private Effect[] _effects;

    //stores IDs for different Effects.
    private enum EffectID
    {
        None, SpeedBoost
    };

    //the amount of EffectID's we have.
    private int _iDCount = 2;

    //changes the current Effect.
    public void Shift(int effectID)
    {
        //if the number passed in is out of bounds, return.
        if (effectID > _effects.Length - 1 || effectID < 0)
            return;


        if((int)_currentEffect != 0)
        //finish early the old effect.
        _effects[(int)_currentEffect].Finish();

        //change the current effect ID
        _currentEffect = (EffectID)effectID;

        if((int)_currentEffect != 0)
        //apply the new effect.
        _effects[(int)_currentEffect].Apply();
    }

    private void Awake()
    {
        //begin with no effect.
        _currentEffect = EffectID.None;

        //initialize a new array for effects.
        _effects = new Effect[_iDCount];

        //store a reference to the Player's speedboost component.
        _effects[(int)EffectID.SpeedBoost] = gameObject.GetComponent<Effect_SpeedBoost>();
    }

    private void Update()
    {
        //return if the effectID is none.
        if(_currentEffect == EffectID.None)
            return;

        //if the time has ran out.
        if (_effects[(int)_currentEffect].TimeLeft <= 0)
        {
            //shift the effect to None.
            Shift((int)EffectID.None);
        }
    }
}
