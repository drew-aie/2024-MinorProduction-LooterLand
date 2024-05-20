using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Collectable : MonoBehaviour
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
