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
    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _cop = GetComponent<NavMeshAgent>();
    }

    // FixedUpdate is called once per fixed framerate frame
    void FixedUpdate()
    {
        _cop.destination = _target.transform.position;
        _cop.transform.position = Vector3.SmoothDamp(_cop.transform.position, _cop.nextPosition, ref _velocity, 0.05f);
    }
}
