using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "spawnArea_region", menuName = "ScriptableObject/spawnArea_region")]
public class spawnArea_regionSO : ScriptableObject
{
    [SerializeField, Tooltip("The areas in this region to spawn items.")]
    private List<GameObject> _spawnAreas;

    //property for _spawnAreas. [Read-Only]
    public List<GameObject> SpawnAreas
    {
        get => _spawnAreas;
    }
}
