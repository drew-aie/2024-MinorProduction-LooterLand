using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Region : MonoBehaviour
{
    [SerializeField, Tooltip("The asset that holds the list of areas in this region.")]
    private SpawnArea_regionSO _regionData;

    //property for _regionData. Gets/sets the collection of spawn areas.
    public SpawnArea_regionSO RegionData
    {
        get => _regionData;
        set => _regionData = value;
    }
    
    [SerializeField, Tooltip("The player that this region is waiting for.")]
    private GameObject _player;

    //spawns items when player arrives
    public UnityEvent OnPlayerEnter;
    //despawns items when player leaves
    public UnityEvent OnPlayerExit;

    private void OnTriggerEnter(Collider collider)
    {
        //if the Player has entered this region, change his region to this and invoke OnPlayerEnter.
        if (collider.gameObject.TryGetComponent<PlayerRegion>(out PlayerRegion playerRegion))
        {
            playerRegion.Region = this;
            OnPlayerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //if the Player has left this region, change it's region to null and invoke OnPlayerExit.
        if (collider.gameObject.TryGetComponent<PlayerRegion>(out PlayerRegion playerRegion))
        {
            playerRegion = null;
            OnPlayerExit.Invoke();
        }
    }

}
