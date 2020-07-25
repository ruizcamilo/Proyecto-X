using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    // Chasing support
    public float speed;
    public LayerMask playerLayer;
    public float checkDistance;
    public float chaseDistance;
    public float attackDistance;

    private bool _waiting = true;
    private GameObject _player;
    private Vector2 _direction;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Check player
        if (_waiting)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, checkDistance, playerLayer);
            if (hitInfo.collider != null)
            {
                _waiting = false;
                _animator.SetBool("Awake", true);
                _animator.SetBool("Idle", true);
                StartCoroutine("ChasePlayer");
            }
        }

    }

    private void LateUpdate()
    {
        if (!_waiting && _player != null)
        {
            if (_player.transform.position.x >= transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    IEnumerator ChasePlayer()
    {
        yield return new WaitForSeconds(2f);

        _animator.SetBool("Idle", false);
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        while (distance <= chaseDistance)
        {
            if(distance > attackDistance)
            {
                _direction = _player.transform.position - transform.position;
                _rigidbody.velocity = new Vector2(_direction.normalized.x * speed, _rigidbody.velocity.y);
                yield return null;
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
                _animator.SetTrigger("Attack");
                _animator.SetBool("Idle", true);

                yield return new WaitForSeconds(1.5f);
                _animator.SetBool("Idle", false);
            }
            distance = Vector2.Distance(transform.position, _player.transform.position);
        }

    }

}
