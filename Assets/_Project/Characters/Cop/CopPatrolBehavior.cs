using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CopPatrolBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _navPoints;

    private NavMeshAgent _cop;
    private NavMeshPath _path;
    private EState _currentState = EState.IDLE;

    private int _navIter;

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
            TransitionTo(EState.PATROL);
            return;
        }
        else if (_currentState == EState.PATROL)
        {
            if (!_hasReachedPath)
            PatrolPath();
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

    private void PatrolPath()
    {
        if (!_patrolStarted)
        {
            _navIter = -1;
            _patrolStarted = true;
        }
        _navIter++;

        if (!_hasReachedPath)
            return;

        _cop.destination = _navPoints[_navIter].transform.position;

        if (_hasReachedPath)
            TransitionTo(EState.IDLE);

    }

    private void Wander()
    {
        //Wander
    }
}
