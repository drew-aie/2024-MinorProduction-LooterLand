using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopCollision : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is the amount of time in Seconds to wait.")]
    float _timeToWait = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        PlayerCollided(collision.gameObject);

        if (TryGetComponent(out NavMeshAgent agent))
        {
           agent.enabled = false;

           Invoke(nameof(ReEnableAgent), _timeToWait);
        }
         
    }

    //Using OnCollisionStay because Enter causes issues with PlayerCollision script
    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        PlayerCollided(collision.gameObject);
    }

    private void PlayerCollided(GameObject obj)
    {
        //hurt the player
        obj.gameObject.GetComponent<PlayerCollision>().HurtPlayer(gameObject);

        //Storing player's Player Collision component
        PlayerCollision targetCollide = obj.gameObject.GetComponent<PlayerCollision>();

        //Disabling cop's collider for the length of the protection period duration
        DisableCopCollider(targetCollide.ProtectionPeriodDuration);
    }

    private void ReEnableAgent()
    {
        if (TryGetComponent(out NavMeshAgent agent))
        {
            agent.enabled = true;
        }
    }

    /// <summary>
    /// Disables the agent's collider for the entered amount of time after making contact with the player.
    /// </summary>
    /// <param name="safetyTime">How long the agent's collider will be disabled.</param>
    public void DisableCopCollider(float safetyTime)
    {
        //Storing agent's collider
        CapsuleCollider collider = GetComponent<CapsuleCollider>();

        if (!collider)
            return;

        //Disabling collider
        collider.enabled = false;

        //Starting a coroutine to reenable collider after safety time elapses
        StartCoroutine(Delay(() => { collider.enabled = true; }, safetyTime));
    }

    private IEnumerator Delay(Action callback, float delay)
    {
        yield return new WaitForSeconds(delay);
        callback();
    }
}
