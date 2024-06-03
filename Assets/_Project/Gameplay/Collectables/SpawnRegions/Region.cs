using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
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


    [SerializeField, Tooltip("Group of common items.")]
    private collectable_collectionSO _common;
    [SerializeField, Tooltip("Group of uncommon items.")]
    private collectable_collectionSO _uncommon;
    [SerializeField, Tooltip("Group of rare items.")]
    private collectable_collectionSO _rare;

    [Space]

    [SerializeField, Tooltip("Spawn percentage of uncommon items."), Range(1, 50)]
    private float _uncommonChancePercentage;

    [SerializeField, Tooltip("Spawn percentage of rare items."), Range(1, 30)]
    private float _rareChancePercentage;

    //calculated at runtime.
    private float _commonChancePercentage;

    //items that have be instantialized.
    private List<GameObject> _spawnedItems;

    //spawns items when player arrives
    public UnityEvent OnPlayerEnter;

    //despawns items when player leaves
    public UnityEvent OnPlayerExit;

    private void Awake()
    {
        //find the percent value leftover for common items after uncommon and rare item chance has been set.
        _commonChancePercentage = 100 - _uncommonChancePercentage - _rareChancePercentage;

        //instantialize the _spawnedItems list.
        _spawnedItems = new List<GameObject>();
    }

    public void Refresh()
    {
        //seed the random number generator.
        Seed();

        //spawns new items.
        for (int i = 0; i < _regionData.SpawnAreas.Count; i++)
        {
            //gets a random percentage for our spawn chance.
            int randomPercentage = UnityEngine.Random.Range(0, 100);

            //creates a container to store the collection we will chose.
            collectable_collectionSO collectionToSpawnFrom;

            //determine which group of objects to pull from based on our randomPercentage value.
            if (randomPercentage <= _commonChancePercentage)
            {
                collectionToSpawnFrom = _common;
            }
            //combine our common chance and uncommon chance to get the threshold that the percentage will have to be within to pull from uncommon.
            else if (randomPercentage <= _commonChancePercentage + _uncommonChancePercentage)
            {
                collectionToSpawnFrom = _uncommon;
            }
            //if it is not in the uncommon range then pull from the rare group.
            else
            {
                collectionToSpawnFrom = _rare;
            }

            //gets a random item index.
            int randomItemIndex = UnityEngine.Random.Range(0, collectionToSpawnFrom.CollectableItems.Count);
            //gets a random item in the collection we chose randomly.
            GameObject itemToInstantiate = collectionToSpawnFrom.CollectableItems[randomItemIndex];

            //container to hold our newly spawned item.
            GameObject spawnedItem;
            //instantiates that item at the position of the spawn area and store it.
            spawnedItem = Instantiate(itemToInstantiate, _regionData.SpawnAreas[i].transform.position, _regionData.SpawnAreas[i].transform.rotation);
            //add the instantialized item to our list of _spawnedItems.
            _spawnedItems.Add(spawnedItem);
        }
    }

    public void DespawnItems()
    {
        //continue while there are items that still exist.
        while (0 < _spawnedItems.Count)
        {
            //get the first item in the array.
            GameObject itemToDelete = _spawnedItems[0];
            //remove it from the array of spawned items.
            _spawnedItems.Remove(itemToDelete);
            //then destroy the item completely.
            Destroy(itemToDelete);
        }
    }

    private void Seed()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    }

    private void OnTriggerEnter(Collider collider)
    {
        //if the Player has entered this region.
        if (collider.gameObject.TryGetComponent<PlayerRegion>(out PlayerRegion playerRegion))
        {
            //spawn items
            Refresh();
            //add this region to the list of regions the player is in
            playerRegion.Regions.Add(this);
            //invoke OnPlayerEnter
            OnPlayerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //if the Player has left this region.
        if (collider.gameObject.TryGetComponent<PlayerRegion>(out PlayerRegion playerRegion))
        {
            //despawn items
            DespawnItems();
            //remove this region to the player's region list
            playerRegion.Regions.Remove(this);
            //invoke OnPlayerExit
            OnPlayerExit.Invoke();
        }
    }

}
