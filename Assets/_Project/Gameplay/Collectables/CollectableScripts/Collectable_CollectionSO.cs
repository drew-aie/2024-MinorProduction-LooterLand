using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable_Collection", menuName = "ScriptableObject/Collectable_CollectionSO")]
public class Collectable_CollectionSO : ScriptableObject
{

    [SerializeField, Tooltip("The list of collectables that this stores.")]
    private List<GameObject> _collectableItems;

    //property for _collectablesItems [Read-Only] 
    public List<GameObject> CollectableItems
    {
        get { return _collectableItems; }
    }
}
