using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrulla : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad;
    public float damage;

    private bool movDerecha = false;
    public LayerMask pisoLayerMask;

    public Transform detectaSuelo;
    public CircleCollider2D cc;
    public BoxCollider2D pared;
    public SpriteRenderer Oldi;
    public Sprite second;
    private void Update()
    {
        
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
       // _hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, .1f, pisoLayerMask);

        RaycastHit2D infoSuelo = Physics2D.Raycast(detectaSuelo.position, Vector2.down);
        if(cc!=null&&cc.IsTouching(pared))
        {
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

    public void Death()
    {
        Animator obs = GetComponent<Animator>();
        velocidad = 0;
        obs.enabled = false;
        Oldi.sprite = second;
    }

}
