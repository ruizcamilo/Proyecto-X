using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    private Rigidbody2D rgb;
    float speed = 10f;
    public float life;
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();

        rgb.velocity = new Vector2(speed * transform.rotation.y,0);
        this.life = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        rgb.velocity = new Vector2(speed, 0);
        this.life++;
        if(this.life == 5f)
        {
            Destroy(gameObject);
        }

    }
    
}
