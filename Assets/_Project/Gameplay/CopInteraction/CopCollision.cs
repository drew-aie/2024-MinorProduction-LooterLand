using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopCollision : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is the amount of time in Seconds to wait.")]
    float _timeToWait = 3;

    

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {

            if(TryGetComponent(out NavMeshAgent agent))
            {
                agent.enabled = false;

                Invoke(nameof(ReEnableAgent), _timeToWait);
            }

            

        }


    }

    private void ReEnableAgent()
    {
        if (TryGetComponent(out NavMeshAgent agent))
        {
            agent.enabled = true;
        }
    }

}
