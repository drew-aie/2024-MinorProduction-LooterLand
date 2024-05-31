using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRegion : MonoBehaviour
{
    //the regions the player is currently in.
    private List<Region> _currentRegions;

    //property for _currentRegions.
    public List<Region> Regions
    {
        get => _currentRegions;
        set => _currentRegions = value;
    }

}
