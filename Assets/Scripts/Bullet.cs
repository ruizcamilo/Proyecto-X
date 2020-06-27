using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float speed = 20f;
    public float maxlife = 5f;
    public float life;
    public Rigidbody2D rb;
    public GameObject BulletEnd;
    void Start()
    {
        rb.velocity = transform.right * speed;
        this.life = 0f;
    }

    void Update()
    {
        this.life = this.life + 1;
        if (this.life == this.maxlife)
        {
            Destroy(gameObject);
            Instantiate(BulletEnd, rb.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Player" || hitInfo.name == "Circulo" || hitInfo.name == "Bullet(Clone)")
        {
            //Debug.Log("No hit");
        }
        else{
            //Debug.Log(hitInfo.name);
            Destroy(gameObject);
            Instantiate(BulletEnd, rb.position, Quaternion.identity);
        }
    }
}
