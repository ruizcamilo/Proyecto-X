using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float hp = 40;
    private float damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.CompareTag("Bullet"))
        {
            Bullet bul = hitInfo.GetComponent<Bullet>();
            if (bul != null)
            {
                hp -= bul.damage;
            }
            else
            {
                hp -= hitInfo.GetComponent<SuperShot>().damage;
            }

            if (hp < 0)
            {
                transform.SendMessageUpwards("Death");
            }
        }
        if (hitInfo.CompareTag("Player"))
        {
            hitInfo.GetComponent<PlayerController>().Damage(damage);
        }
    }
}
