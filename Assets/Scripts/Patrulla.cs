using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrulla : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad;

    private bool movDerecha = false;

    public Transform detectaSuelo;

    public CircleCollider2D cc;
    public BoxCollider2D pared;
    public SpriteRenderer Oldi;
    public BoxCollider2D bullet;
    public Sprite second;
    private int hp = 5;
    private void Update()
    {
        
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        
        RaycastHit2D infoSuelo = Physics2D.Raycast(detectaSuelo.position, Vector2.down);
        Animator obs = GetComponent<Animator>();
        if(hp==0)
        {
            velocidad = 0;
            obs.enabled = false;
            Oldi.sprite=second;

        }
        if(cc.IsTouching(pared))
        {
            print("si");
            transform.eulerAngles = new Vector3(0, -180, 0);
            movDerecha = false;
        }
        if (infoSuelo.collider == false)
        {
            if(movDerecha)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movDerecha = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movDerecha = true;
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Bullet" ||  hitInfo.name == "Bullet(Clone)")
        {
            hp -= 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
