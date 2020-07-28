using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valorMoneda = 1;
    public bool taken = false;
    public GameObject explosion;

    // if the player touches the coin, it has not already been taken, and the player can move (not dead or victory)
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

            // do the player collect coin thing
            other.gameObject.GetComponent<PlayerController>().recolectarMonedas(valorMoneda);

            // destroy the coin
            Destroy(this.gameObject);
        }
    }
}
