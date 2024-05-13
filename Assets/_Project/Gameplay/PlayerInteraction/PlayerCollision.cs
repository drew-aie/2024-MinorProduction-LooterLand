using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private ScoreSystem _scoreSystem;
 


    private void OnCollisionEnter(Collision collision)
    {
       
        if(collision.gameObject.TryGetComponent<Collectable>(out Collectable item))
        {
         
            _scoreSystem.IncreaseScore(item.Data.CashValue);
            Destroy(item);
            Debug.Log("is hitting item");
        }

        else if(collision.gameObject.CompareTag("Enemy"))
        {
            _scoreSystem.ReduceScore();
            Debug.Log("is hitting enemy");
        }
            

        
    }
  //  public bool CompareTag(string tag)
 //   {
   //     return gameObject.CompareTag(tag);
   // }










}
