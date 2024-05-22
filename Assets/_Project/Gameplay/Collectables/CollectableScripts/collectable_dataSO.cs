using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "collectable_data", menuName = "ScriptableObject/collectable_data")]
public class collectable_dataSO : ScriptableObject
{
    //the id of this item, set by the tiering ScriptableObject.
    private float _id;
    //property for _id
    public float ID
    {
        get { return _id; }
        set { _id = value; }
    }
    [SerializeField, Tooltip("The name of this item.")]
    private string _name;

    [SerializeField, Tooltip("The cash value of this item.")]
    private float _cashValue;

    public float CashValue
    {
        get { return _cashValue; }
    }
}
