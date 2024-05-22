using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CopPatrolBehavior : MonoBehaviour
{
    [SerializeField, Tooltip("What the cop will chase once it enters it's radius. (The Player)")]
    private GameObject _target;

    [Space]

    [SerializeField, Tooltip("Stores the places on the map the cop will move to. Can be objects with mesh renders and colliders turned off.")]
    private GameObject[] _navPoints;

    private NavMeshAgent _cop;

    private EState _currentState = EState.IDLE;

    private int _navIter;
    private float _idleTime = 0;
    private float _bufferTime = 0;

    private bool _patrolStarted = false;
    private bool _hasReachedPath = true;
    private bool _inMotion = false;

    //Enum holding behavior states
    enum EState
    {
        IDLE,
        PATROL,
        WANDER,
        PURSUE,
        END 
    };

    // Start is called before the first frame update
    void Start()
    {
        _cop = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState == EState.IDLE)
        {
            _idleTime += Time.deltaTime;

            if (_idleTime >= 2)
                TransitionTo(EState.PATROL);

            return;
        }
        else if (_currentState == EState.PATROL)
        {
            //Resetting idle timer
            _idleTime = 0;

            PatrolPath();
            MotionCheck();

            return;
        }
        else if (_currentState == EState.WANDER)
            Wander();
        else if (_currentState == EState.PURSUE)
            Pursue();
        else
            return;
    }

    /// <summary>
    /// Transitions from one state to another.
    /// </summary>
    /// <param name="state">The behavior that will become the current state.</param>
    private void TransitionTo(EState state)
    {
        //Guard Clause
        if (state == EState.END || state == _currentState)
            return;

        _currentState = state;
    }

    //Checks if agent has reached it's destination and isn't moving
    private void MotionCheck()
    {
        //Setting boolean once the buffer has eclipsed 0.5
        if (_bufferTime >= 0.5f)
            _inMotion = false;

        //Guard Clause
        if (!_hasReachedPath && !_inMotion)
        {
            _hasReachedPath = true;

            //Resetting timer
            _bufferTime = 0;

            //Having agent idle after reaching point
            TransitionTo(EState.IDLE);
        }
    }

    //Handles the functionality for the agent's patrolling behavior
    private void PatrolPath()
    {
        //Checking if PatrolPath is being called for the first time
        if (!_patrolStarted)
        {
            _navIter = -1;
            _patrolStarted = true;
        }

        //Creating a buffer once agent stops moving to not immediately skip idle
        if (_cop.velocity == Vector3.zero)
            _bufferTime += Time.deltaTime;

        //Don't run if agent hasn't reached path
        if (!_hasReachedPath)
            return;

        //Guard so iter doesn't exceed the bounds of the nav points array
        if (_hasReachedPath && _navIter == _navPoints.Length - 1)
            _navIter = 0;
        else
            //Incrementing each time PatrolPath is called
            _navIter++;

        //Setting agents destination to be position of current patrol point
        _cop.destination = _navPoints[_navIter].transform.position;

        _inMotion = true;
        _hasReachedPath = false;
    }

    private void Wander()
    {
        //Wander
    }

    private void Pursue()
    {
        //One pleaseburger cheese
        Rigidbody targetRigid = _target.GetComponent<Rigidbody>();

        _cop.destination += _target.transform.position + targetRigid.velocity.normalized;
    }
}
