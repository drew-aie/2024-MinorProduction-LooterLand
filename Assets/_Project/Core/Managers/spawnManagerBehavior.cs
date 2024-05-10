using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManagerBehavior : MonoBehaviour
{
    //the region the player is in.
    private SpawnArea_regionSO _currentRegion;

    [SerializeField, Tooltip("The player.")]
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

    private void Awake()
    {
        //find the percent value leftover for common items after uncommon and rare item chance has been set.
        _commonChancePercentage = 100 - _uncommonChancePercentage - _rareChancePercentage;

        //instantialize the _spawnedItems list.
        _spawnedItems = new List<GameObject>();
    }

    public void Refresh()
    {
        //despawns items from the previous area.
        //does nothing if there are no items referenced.
        DespawnItems();

        //sets the current region to the region the player is in.
        SetCurrentRegion();

        //return if the player is not in a spawning region.
        if (!_currentRegion)
            return;

        //seed the random number generator.
        Seed();

        //spawns new items.
        for (int i = 0; i < _currentRegion.SpawnAreas.Count; i++)
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
            int randomItemIndex = UnityEngine.Random.Range(0, collectionToSpawnFrom.CollectableItems.Count - 1);
            //gets a random item in the collection we chose randomly.
            GameObject itemToInstantiate = collectionToSpawnFrom.CollectableItems[randomItemIndex];

            //container to hold our newly spawned item.
            GameObject spawnedItem;
            //instantiates that item at the position of the spawn area and store it.
            spawnedItem = Instantiate(itemToInstantiate, _currentRegion.SpawnAreas[i].transform.position, _currentRegion.SpawnAreas[i].transform.rotation);
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

        //sets the Player's region to null if region data is not found.
        SetCurrentRegion();
    }

    private void SetCurrentRegion()
    {
        PlayerRegion playerRegion = _player.GetComponent<PlayerRegion>();

        //if the player's region exists, set the current region
        if (playerRegion.Region)
            _currentRegion = playerRegion.Region.RegionData;
        //else set the region data to null.
        else
            _currentRegion = null;
    }


    void Seed()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    }
}
