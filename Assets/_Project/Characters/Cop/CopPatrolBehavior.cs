using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CopPatrolBehavior : MonoBehaviour
{
    private NavMeshAgent _cop;
    private EState _currentState = EState.IDLE;

    //
    enum EState
    {
        IDLE,
        WANDER,
        END 
    };

    private void TransitionTo(EState state)
    {
        //Guard Clause
        if (state == EState.END || state == _currentState)
            return;

        _currentState = state;

    }

    // Start is called before the first frame update
    void Start()
    {
        _cop = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case EState.IDLE:
                //Check state 
                if (_currentState != EState.IDLE)
                    break;

                break;

            case EState.WANDER:
                //Check state
                if (_currentState != EState.WANDER)
                    break;

                break;

            case EState.END:
                //Check state
                if (_currentState != EState.END)
                    break;

                break;

            default:
                break;
        }
    }
}
