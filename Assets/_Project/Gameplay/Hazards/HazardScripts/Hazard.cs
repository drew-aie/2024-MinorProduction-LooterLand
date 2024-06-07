using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField, Tooltip("The Physics that this Hazard applies to Characters.")]
    private PhysicMaterial _physics;

    [SerializeField, Tooltip("The duration of this Hazard effect on Characters.")]
    private float _duration;

    [SerializeField, Tooltip("True if this should make the player slide.")]
    private bool _isSlippery;

    [SerializeField, Tooltip("The strength of the slip effect on Characters.")]
    private float _slipperyMagnifier;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterHazardBehavior hazardBehavior))
        {
            hazardBehavior.ChangePhysics(_physics, _duration, _isSlippery, _slipperyMagnifier);
        }
    }
}
