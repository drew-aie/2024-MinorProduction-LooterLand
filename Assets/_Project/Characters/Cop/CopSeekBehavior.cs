using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CopSeekBehavior : MonoBehaviour
{
    [SerializeField, Tooltip("What the cop will be chasing. (The Player)")]
    private GameObject _target;

    private NavMeshAgent _cop;

    // Start is called before the first frame update
    void Start()
    {
        _cop = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _cop.destination = _target.transform.position;
    }

}
