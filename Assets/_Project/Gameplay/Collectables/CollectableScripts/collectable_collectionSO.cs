using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "collectable_collection", menuName = "ScriptableObject/collectable_collection")]
public class collectable_collectionSO : ScriptableObject
{
    [SerializeField, Tooltip("The list of collectables that this stores.")]
    private List<GameObject> _collectableItems;

    //property for _collectablesItems [Read-Only] 
    public List<GameObject> CollectableItems
    {
        get { return _collectableItems; }
    }
}
