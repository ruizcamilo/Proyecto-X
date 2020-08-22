using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    private const float checkDistance = 10;
    private const float forceShoot = 30;
    private const float deathTime = 5;

    public LayerMask playerLayer;

    private bool down = false;
    private Timer deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = deathTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!down)
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x - 0.4f, transform.position.y), new Vector2(1, checkDistance), 0, Vector2.down, checkDistance, playerLayer);
            if (hit.collider != null)
            {

                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    Transform child = gameObject.transform.GetChild(i);
                    child.gameObject.SetActive(true);
                    child.GetComponent<Rigidbody2D>().AddForce(Vector2.down * forceShoot, ForceMode2D.Impulse);
                }
                deathTimer.Run();
                down = true;
            }
        }
        else
        {
            if (deathTimer.Finished)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
