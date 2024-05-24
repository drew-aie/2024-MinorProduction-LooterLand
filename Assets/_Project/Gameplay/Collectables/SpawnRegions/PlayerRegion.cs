using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRegion : MonoBehaviour
{
    //the region the player is currently in.
    private Region _currentRegion;

    //property for _currentRegion.
    public Region Region
    {
        get => _currentRegion;
        set => _currentRegion = value;
    }

}
