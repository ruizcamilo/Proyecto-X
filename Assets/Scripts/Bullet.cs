using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float speed = 20f;
    public float maxlife = 5f;
    public float life;

    public GameObject BulletEnd;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        this.life = 0f;
    }

    void Update()
    {
        this.life = this.life + 1;
        if (this.life == this.maxlife)
        {
            Destroy(gameObject);
            Instantiate(BulletEnd, _rigidbody.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Player" || hitInfo.name == "Circulo" || hitInfo.name == "Bullet(Clone)")
        {
           //Debug.Log(hitInfo.name);
        }
        else
        {
            //Debug.Log(hitInfo.name);
            Destroy(gameObject);
            Instantiate(BulletEnd, _rigidbody.position, Quaternion.identity);
        }
    }
}
