using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField, Tooltip("The data values of this collectable.")]
    //stores a reference to this collectable's data.
    private collectable_dataSO _data;

    //property for the _data of this collectable.
    public collectable_dataSO Data
    {
        get { return _data; }
    }

    [SerializeField, Tooltip("The effect this collectable has. Empty if this has no Effect.")]
    private Effect _effect;

    //property for _effect.
    public Effect CollectableEffect
    {
        get => _effect;
    }
}
