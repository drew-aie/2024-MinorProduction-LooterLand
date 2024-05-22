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

    [SerializeField, Min(0.5f), Tooltip("How big the patrolling cop's player detection radius is.")]
    private float _copDetectionRadius = 10;

    [Space]

    [SerializeField, Tooltip("Stores the places on the map the cop will move to. Can be objects with mesh renders and colliders turned off.")]
    private GameObject[] _navPoints;

    private NavMeshAgent _cop;
    private NavMeshPath _patrolPath;

    private EState _currentState = EState.IDLE;

    private int _navIter;
    private float _idleTime = 0;
    private float _bufferTime = 0;

    private bool _patrolStarted = false;
    private bool _hasReachedPath = true;
    private bool _inMotion = false;
    private bool _agentIsSeeking = false;

    private Vector3 _velocity = Vector3.zero;

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
        //Check if player has moved far enough away from agent when seeking
        if (_agentIsSeeking && _cop.remainingDistance > 15)
        {
            //Reset agent path back to patrol path
            _cop.ResetPath();
            _cop.path = _patrolPath;

            //Tell console that agent is not seeking
            _agentIsSeeking = false;

            //Reset idle time and have agent idle
            _idleTime = 0;
            TransitionTo(EState.IDLE);
        }

        //Checking if player is within agro range before seeking
        if (RadiusCheck() && _patrolStarted)
            TransitionTo(EState.PURSUE);

        //If statements that check agent's current state
        if (_currentState == EState.IDLE)
        {
            _idleTime += Time.deltaTime;

            //Stop idling after 2 seconds
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
            return;
        else
            return;
    }

    private void FixedUpdate()
    {
        if (!_agentIsSeeking)
            return;

        Pursue();

        //Smoothing agent's velocity
        _cop.transform.position = Vector3.SmoothDamp(_cop.transform.position, _cop.nextPosition, ref _velocity, 0.05f);
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

    //Tells agent if the player has entered it's agro radius
    private bool RadiusCheck()
    {
        float seekMagni = (_target.transform.position - _cop.transform.position).magnitude;

        if (seekMagni <= _cop.radius * _copDetectionRadius)
        {
            _agentIsSeeking = true;
            return true;
        }
        else
            return false;
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
        _cop.transform.position = Vector3.SmoothDamp(_cop.transform.position, _cop.nextPosition, ref _velocity, 0.05f);

        //Storing current patrol path
        _patrolPath = _cop.path;

        _inMotion = true;
        _hasReachedPath = false;
    }

    private void Pursue()
    {
        //Storing direction to target
        Vector3 targetDirection = _target.transform.position - _cop.transform.position;

        //Storing variables needed for pursue beahvior
        float relativeHead = Vector3.Angle(_cop.transform.forward, _cop.transform.TransformVector(_target.transform.forward));
        float toTarget = Vector3.Angle(_cop.transform.forward, _cop.transform.TransformVector(targetDirection));

        //Checking if to target and relative heading are within needed parameters and player has adequate speed for the behavior
        if ((toTarget > 90 && relativeHead < 20) || _target.GetComponent<Input>().MaxSpeed < 0.01f)
        {
            //Running seek behavior if not
            _cop.destination = _target.transform.position;
            return;
        }

        //Storing force to have agent look ahead of player
        float lookAhead = targetDirection.magnitude / (_cop.speed + _target.GetComponent<Input>().MaxSpeed);

        //Having agent pursue 
        _cop.destination = _target.transform.position + _target.transform.forward * lookAhead;
    }

    private void Wander()
    {
        //Wander
    }
}
