using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailHandler : MonoBehaviour
{
    [SerializeField, Tooltip("The trail renderer attached to this character.")]
    private TrailRenderer _trailRenderer;

    [SerializeField, Tooltip("The new trail color that will be applied to this character.")]
    private Gradient _newTrailColor;

    private Gradient _originalTrailColor;

    private void Awake()
    {
        _originalTrailColor = _trailRenderer.colorGradient;
    }

    public void ApplyNewTrail()
    {
        _trailRenderer.colorGradient = _newTrailColor;
    }

    public void ResetTrail()
    {
        _trailRenderer.colorGradient = _originalTrailColor;
    }
}
