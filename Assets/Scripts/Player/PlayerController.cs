//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float dashSpeed;
    public float speed;
    public float maxDashSpeed;
    public LayerMask pisoLayerMask;
    public Transform groundPoint;
    public float groundRadius;
    public float jumpPower;
    private float healt;
    public float Max_healt;

    //Factores de debuff
    public float debuffTime = 10f;
    public float debuffFactorSlow = 3f;
    public float debuffFactorHeavy = 3f;

    //Esto se hace para que la variable se puede acceder desde otros scripts pero no se muestra en el inspector de Unity
    [HideInInspector]
    public bool playerCanMove = true;

    //Variables privadas
    private Rigidbody2D _rigidbody;
    private Weapon _weapon;
    private Animator _animator;
    private Renderer _renderer;
    private Color _original;

    private BoxCollider2D _box;

    //Debuffs
    private bool _playerIsSlow = false;
    private bool _playerIsHeavy = false;
    private bool _playerIsSingleJump = false;

    private bool _jump, _walk, _dash;
    private bool _grounded;
    private RaycastHit2D _hit;
    private Vector2 _inputAxis;
    private Timer inmunnity;
    // Shooting
    private bool _shoot;
    private bool _fixed;

    // Start is called before the first frame update
    void Start()
    {
        _box = transform.GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _renderer = GetComponent<Renderer>();
        _original = _renderer.material.color;
        _box = GetComponent<BoxCollider2D>();
        healt = Max_healt;
        inmunnity = gameObject.AddComponent<Timer>();
        inmunnity.Duration =0.5f;
        inmunnity.Run();


    }

    // Update is called once per frame
    void Update()
    {
        if (_box.IsTouchingLayers()&&inmunnity.Finished)
        {
            healt -= 1;
            inmunnity.Run();
            //Debug.Log("vida: "+healt);
            if(healt ==0)
            {
                Debug.Log("U DED");
                OnBecameInvisible();
                healt = Max_healt;
            }
        }


        
        _grounded = IsGrounded();
        //Variable para evitar que se hagan actualizaciones si el jugador no se puede mover o el juego está pausado
        //Falta implementar la parte de pausar.
        if (!playerCanMove)
            return;


        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));
        _animator.SetBool("Grounded", _grounded);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _shoot = true;
            _animator.SetBool("Shoot", _shoot);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _fixed = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _fixed = false;
        }

        //_inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_grounded)
        {
            _jump = true;
            _walk = true;
            _dash = true;
        }


    }


    void FixedUpdate()
    {


       
        if (_fixed)
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);

        if (!_fixed && Input.GetKeyDown(KeyCode.W))
        {
            _walk = false;
            if (_grounded)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                //_rigidbody.AddForce(new Vector2(0, jumpPower));

            }
            else
            {
                if (_jump)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                    _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    //_rigidbody.AddForce(new Vector2(0, jumpPower*2));

                    _jump = false;
                }
            }
        }

        float h = Input.GetAxis("Horizontal");
        if (h != 0 && !_fixed)
        {
            /*
            if (Input.GetKeyDown(KeyCode.LeftShift) && _dash)
            {
                Debug.Log("dashprron");
                _rigidbody.velocity = new Vector2(h * dashSpeed, _rigidbody.velocity.y);
                _dash = false;

            }
            else
            {
                _rigidbody.AddForce(Vector2.right * speed * h);

                float limitedSpeed = Mathf.Clamp(_rigidbody.velocity.x, -maxSpeed, maxSpeed);
                _rigidbody.velocity = new Vector2(limitedSpeed, _rigidbody.velocity.y);
            }*/

            _rigidbody.AddForce(Vector2.right * speed * h);

            float limitedSpeed = Mathf.Clamp(_rigidbody.velocity.x, -maxSpeed, maxSpeed);
            _rigidbody.velocity = new Vector2(limitedSpeed, _rigidbody.velocity.y);

            
        }

        if (h > 0.1f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (h < -0.1f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (_shoot)
        {
            _animator.SetBool("Shoot", false);
        }

    }

    private void LateUpdate()
    {
        setColor();
    }

    //Slows down or returns the player to its orginal movement speed by a defined factor
    public void slowDownPlayer()
    {
        _playerIsSlow = true;
        maxSpeed /= debuffFactorSlow;
        StartCoroutine(slowDownPlayerRoutine());
    }

    //Slow down player coroutine
    IEnumerator slowDownPlayerRoutine()
    {
        yield return new WaitForSecondsRealtime(debuffTime);
        maxSpeed *= debuffFactorSlow;
        _playerIsSlow = false;
    }

    //Makes the player heavier 
    public void heavierPlayer()
    {

        _playerIsHeavy = true;
        _rigidbody.gravityScale *= debuffFactorHeavy;
        StartCoroutine(heavierPlayerRoutine());
    }

    //Make player heavier coroutine
    IEnumerator heavierPlayerRoutine()
    {
        yield return new WaitForSecondsRealtime(debuffTime);
        _rigidbody.gravityScale /= debuffFactorHeavy;
        _playerIsHeavy = true;

    }

    void OnBecameInvisible()
    {
        transform.position = new Vector3(-2, 0, 0);
    }

    void setColor()
    {
        switch (this._weapon.type)
        {
            case 0:
                this._renderer.material.color = _original;
                break;
            case 1:
                this._renderer.material.color = Color.red;
                break;
            case 2:
                this._renderer.material.color = Color.blue;
                break;
            default:
                break;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundPoint.position, groundRadius, pisoLayerMask);
    }


    
}
