using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField, Tooltip("The ID of the effect this item applies. " +
                             "0 for None, 1 for SpeedBoost.")]
    private int _effectID;
    //property for _effectID
    public int EffectID
    {
        get { return _effectID; }
    }
    [SerializeField, Tooltip("The name of this item.")]
    private string _name;

    [SerializeField, Tooltip("The cash value of this item.")]
    private float _cashValue;

    //property for _cashValue.
    public float CashValue
    {
        get { return _cashValue; }
        set { _cashValue = value; }
    }

    //holds the collider of this GameObject.
    private BoxCollider _collider;


    private void Awake()
    {
        //stores the collider for this GameObject.
        _collider = GetComponent<BoxCollider>();
    }

    public void DelayPickup()
    {
        //disable the collider of the GameObject.
        _collider.enabled = false;

        //wait to reenable after 1 second
        Invoke("EnablePickup", 1f);
    }

    private void EnablePickup()
    {
        //enables the collider for this GameObject.
        _collider.enabled = true;
    }
}
