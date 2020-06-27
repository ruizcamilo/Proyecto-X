using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatroling : MonoBehaviour
{
    public float speed = 10f;
    public GameObject first;
    public GameObject second;
    public float life = 100f;
    public float waitingTime = 2f;
    public float reactRange = 1f;
    public float chaseRange = 4f;

    private GameObject _target;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Transform _ladderTarget;
    private Transform _player;
    private bool _isReacting;
    private bool _isChasing;
    private bool _isAttacking;
    private bool _inLadder;
    private bool _takingDamage;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        if(GameObject.FindGameObjectWithTag("Player") != null)
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateDirection();
        StartCoroutine("PatrolToTarget");

    }

    // Update is called once per frame
    void Update()
    { 
        if(_player != null && !_isChasing && !_inLadder && Vector2.Distance(transform.position, _player.position) < reactRange)
        {
            if (!_isReacting)
            {
                StopAllCoroutines();

                _animator.SetTrigger("React");
                _animator.SetBool("Idle", false);
                _rigidbody.velocity = Vector2.zero;
                _isReacting = true;
            }
        }

        if (_isChasing )
        {
            if (!_isAttacking && Vector2.Distance(transform.position, _player.position) < 1f)
            {
                _animator.SetTrigger("Attack");
                _rigidbody.velocity = Vector2.zero;
                _isAttacking = true;
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.11f);
            }
            else if (!_isAttacking && Vector2.Distance(transform.position, _player.position) < chaseRange)
            {
                Vector2 direction = _player.position - transform.position;

                _animator.SetBool("Idle", false);
                _rigidbody.velocity = new Vector2(direction.normalized.x * speed, _rigidbody.velocity.y);
            }
            else if(!_isAttacking)
            {
                speed *= 0.5f;
                _isChasing = false;
                UpdateDirection();
                StartCoroutine("PatrolToTarget");
            }
            
        }

    }

    private void LateUpdate()
    {
        if (_isReacting && !_animator.GetCurrentAnimatorStateInfo(0).IsTag("React"))
        {
            _isChasing = true;
            _isReacting = false;
            speed *= 2;
        }
        if (_isChasing)
        {
            if(_player.position.x >= transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        if (_isAttacking && !_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = false;
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.11f);
        }
    }

    IEnumerator PatrolToTarget()
    {
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.25f)
        {            
            Vector2 direction = _target.transform.position - transform.position;

            _animator.SetBool("Idle", false);
            _rigidbody.velocity = new Vector2(direction.normalized.x * speed, _rigidbody.velocity.y);
            yield return null;
        }

        transform.position = new Vector2(_target.transform.position.x, transform.position.y);
        _rigidbody.velocity = Vector2.zero;
        UpdateDirection();

        _animator.SetBool("Idle", true);

        yield return new WaitForSeconds(waitingTime);

        StartCoroutine("PatrolToTarget");
    }

    private void UpdateDirection()
    {
        if (_target == null)
        {
            _target = first;
            if(transform.position.x <= first.transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        
        else if (_target.transform.position.x == first.transform.position.x)
        {
            _target = second;
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (_target.transform.position.x == second.transform.position.x)
        {
            _target = first;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            _target = first;
            if (transform.position.x <= first.transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void moveInLadder(Transform endLadder)
    {
        if(!_isReacting && !_isChasing && 
            Vector2.Distance(endLadder.position, _target.transform.position) < Vector2.Distance(transform.position, _target.transform.position))
        {
            StopAllCoroutines();
            _ladderTarget = endLadder;
            _inLadder = true;

            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            StartCoroutine("MovingInLadder");

        }
    }

    IEnumerator MovingInLadder()
    {
        while (Vector2.Distance(transform.position, _ladderTarget.transform.position) > 0.025f)
        {
            _animator.SetBool("Idle", true);

            Vector2 direction = _ladderTarget.transform.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            yield return null;
        }

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1f);
        _animator.SetBool("Idle", false);

        StartCoroutine("PatrolToTarget");
    }

    public void takeDamage( float damage )
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            Bullet bullet = collision.GetComponentInChildren<Bullet>();
            takeDamage(bullet.damage);
        }
    }

}
