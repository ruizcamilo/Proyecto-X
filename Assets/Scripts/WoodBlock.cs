using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlock : MonoBehaviour
{
    private const float checkDistance = 10;
    private bool down = false;
    private Rigidbody2D _rb2d;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!down)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, checkDistance, playerLayer);
            if (hit.collider != null)
            {
                _rb2d.gravityScale = 1;
                down = true;
            }
        }
        
    }
}
