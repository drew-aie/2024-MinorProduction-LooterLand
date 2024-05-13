using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnArea_region", menuName = "ScriptableObject/SpawnArea_region")]
public class SpawnArea_regionSO : ScriptableObject
{
    [SerializeField, Tooltip("The areas in this region to spawn items.")]
    private List<GameObject> _spawnAreas;

    //property for _spawnAreas.
    public List<GameObject> SpawnAreas
    {
        get => _spawnAreas;
        set => _spawnAreas = value;
    }
}
