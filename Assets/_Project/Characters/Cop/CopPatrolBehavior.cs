using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CopPatrolBehavior : MonoBehaviour
{
    [Tooltip("Stores the places on the map the cop will move to. Can be objects with mesh renders turned off.")]
    [SerializeField]
    private GameObject[] _navPoints;

    private NavMeshAgent _cop;
    private NavMeshPath _path;
    private EState _currentState = EState.IDLE;

    private int _navIter;
    private float _time = 0;

    private bool _patrolStarted = false;
    private bool _hasReachedPath = true;

    //Enum holding behavior states
    enum EState
    {
        IDLE,
        PATROL,
        WANDER,
        END 
    };

    void StateMachine()
    {
        //switch (_currentState)
        //{
        //    case EState.IDLE:
        //        //Check state 
        //        if (_currentState != EState.IDLE)
        //            break;

        //        break;

        //    case EState.WANDER:
        //        //Check state
        //        if (_currentState != EState.WANDER)
        //            break;

        //        break;

        //    case EState.END:
        //        //Check state
        //        if (_currentState != EState.END)
        //            break;

        //        break;

        //    default:
        //        break;
        //}
    }

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
            _time += Time.deltaTime;
            Debug.Log("Waiting");

            if (_time >= 2)
                TransitionTo(EState.PATROL);

            return;
        }
        else if (_currentState == EState.PATROL)
        {
            _time = 0;

            PatrolPath();
            MotionCheck();
            
            return;
        }
        else if (_currentState == EState.WANDER)
            Wander();
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
        //Guard Clause
        if (!_hasReachedPath && _cop.velocity == Vector3.zero)
        {
            _hasReachedPath = true;

            //Having agent idle after reaching point
            TransitionTo(EState.IDLE);
            Debug.Log("Test");
        }
    }

    private void PatrolPath()
    {
        //Checking if PatrolPath is being called for the first time
        if (!_patrolStarted)
        {
            _navIter = -1;
            _patrolStarted = true;
        }

        //Guard Clause
        if (!_hasReachedPath)
            return; 
   
        //Incrementing each time PatrolPath is called
        _navIter++;
        Debug.Log(_navIter);
        
        //Guard so iter so it doesn't exceed the bounds of the nav points array
        if (_hasReachedPath && _navIter > _navPoints.Length)
            _navIter = -1;

        //Setting agents destination to be position of current patrol point
        _cop.destination = _navPoints[_navIter].transform.position;

        _hasReachedPath = false;
    }

    private void Wander()
    {
        //Wander
    }
}
