using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_SpeedBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EffectHandler effectHandler))
        {
            //shifts the effect experienced by the player to the SpeedBoost Effect.
            effectHandler.Shift(1);
        }
    }
}
