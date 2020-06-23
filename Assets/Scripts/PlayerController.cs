//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxspeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;

    private Rigidbody2D rb2d;
    private Weapon w;
    private Animator anim;
    private Renderer rend; 
    private Color original;
    
    private bool jump;
    private bool shoot;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        w = GetComponent<Weapon>();
        rend = GetComponent<Renderer>();
        original = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);

        setColor();

        if(Input.GetButtonDown("Fire1"))
        {
            shoot = true;
            anim.SetBool("Shoot", shoot);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate(){
        float h = Input.GetAxis("Horizontal");

        rb2d.AddForce(Vector2.right * speed * h);
        
        float limitedSpeed=Mathf.Clamp(rb2d.velocity.x,-maxspeed,maxspeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
        
        if(h > 0.1f)
        {
            transform.eulerAngles = new Vector3(0, 0 ,0);
        }

        if(h < -0.1f)
        {
            transform.eulerAngles = new Vector3(0, 180 ,0);
        }

        if(jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

        if(shoot)
        {
            anim.SetBool("Shoot", false);
        }

    }

    void OnBecameInvisible(){
        transform.position = new Vector3(-2,0,0);
    }

    void setColor()
    {
        switch (this.w.type)
        {
            case 0:
                this.rend.material.color = original;
                break;
            case 1:
                this.rend.material.color = Color.red;
                break;
            case 2:
                this.rend.material.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
