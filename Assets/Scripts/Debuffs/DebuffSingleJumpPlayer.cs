using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSingleJumpPlayer : MonoBehaviour
{
    public bool taken = false;
    public GameObject explosion;

    // if the player touches the object, it has not already been taken, and the player can move (not dead or victory)
    // then take the coin
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player") && (!taken) && (other.gameObject.GetComponent<PlayerController>().playerCanMove))
        {
            // mark as taken so doesn't get taken multiple times
            taken = true;

            // if explosion prefab is provide, then instantiate it
            if (explosion)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            // Debuff the player with slow movement
            //other.gameObject.GetComponent<PlayerController>().singleJumpPlayer();

            // destroy the object
            Destroy(this.gameObject);
        }
    }
}
