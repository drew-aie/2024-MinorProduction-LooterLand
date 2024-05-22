using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Collectable_Effect_DataSO", menuName = "ScriptableObject/Collectable_Effect_DataSO")]
public class Collectable_Effect_DataSO : ScriptableObject
{
    [SerializeField, Tooltip("The amount of seconds before this effect ends.")]
    private float _endTime;

    //property for _endTime. [Read-Only]
    public float EndTime
    {
        get { return _endTime; }
    }

    [SerializeField, Tooltip("The speed magnification value of this effect.")]
    private float _speedMagnifier;

    //property for _speedMagnifier. [Read-Only]
    public float SpeedMagnifier
    {
        get { return _speedMagnifier; }
    }
}
