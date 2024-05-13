using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Manager that keeps track of increasing and decreasing of player score.")]
    private ScoreSystem _scoreSystem;
 


    private void OnCollisionEnter(Collision collision)
    {
       //if the object that the player is colliding with is a collectable.
        if(collision.gameObject.TryGetComponent<Collectable>(out Collectable item))
        {
            //then
            //increase the current score by the cash value of item
            _scoreSystem.IncreaseScore(item.Data.CashValue);
            
            //remove the item in the scene
            Destroy(item.gameObject);

           
        }

        //if the object that the player is colliding with is an enemy
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            //then
            //reduce the current score by the reduce score value.
            _scoreSystem.ReduceScore();
         

        }
            

        
    }











}
