using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBell : MonoBehaviour
{
    public float damage = 10f;
    public Vector2 initialposition;
    public bool movingBack;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.initialposition = transform.position;
    }

    void Update()
    {
        if(movingBack)
        {
            transform.position=Vector2.MoveTowards (transform.position, initialposition, 10f * Time.deltaTime);
        }
        if(transform.position.y == initialposition.y)
        {
            movingBack = false;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals ("Player"))
        {
            rb.isKinematic = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name.Equals ("Player"))
        {
            PlayerController p = col.gameObject.GetComponent<PlayerController>();
            p.Damage(this.damage);
        }
        if(rb.velocity == Vector2.zero)
        {
            Invoke("GetPlatformBack", 0.5f);
        }
    }

    void GetPlatformBack()
    {
        rb.isKinematic = true;
        movingBack = true;
    }
}
