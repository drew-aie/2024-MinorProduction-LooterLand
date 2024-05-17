using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerToOrigin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if the tag of this gameobject we collided with is the player, move the player's position to the orgin of the scene.
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector3(0, 1, 0);
        }
    }
}
