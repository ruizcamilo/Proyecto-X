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
    public BoxCollider2D bc;
    public SpriteRenderer Oldi;
    public GameObject moneda;
    private float hp = 40;

    void Start()
    {
        Debug.Log("Start called");
        StartCoroutine(startGameCoroutine());
    }

    IEnumerator startGameCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void Update()
    {

        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        // _hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, .1f, pisoLayerMask);

        RaycastHit2D infoSuelo = Physics2D.Raycast(detectaSuelo.position, Vector2.down);
        Animator obs = GetComponent<Animator>();
        if (hp <= 0)
        {
            velocidad = 0;
            obs.enabled = false;
            Instantiate(moneda, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);

        }

        if (infoSuelo.collider == false)
        {
            if (movDerecha)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 12 && movDerecha)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movDerecha = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -0, 0);
            movDerecha = true;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Animator obs = GetComponent<Animator>();
        velocidad = 0;
        obs.enabled = false;
        Oldi.sprite = second;
    }

}
