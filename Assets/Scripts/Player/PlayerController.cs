//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject UI;
    [HideInInspector]
    public UI_Controller scriptUI;

    public float maxSpeed;
    public float dashSpeed;
    public float speed;
    public float maxDashSpeed;
    public LayerMask pisoLayerMask;
    public Transform groundPoint;
    public float groundRadius;
    public float jumpPower;
    public string ability1;
    public string ability2;

    private float health;
    public float Max_health;

    public int monedas; 

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
    private Vector2 _movement;

    //Debuffs
    private bool _playerIsSlow = false;
    private bool _playerIsHeavy = false;
    private bool _playerIsSingleJump = false;

    // States
    private bool _jump, _walk, _dash;
    private bool _grounded;
    private bool _fixed;
    private bool _facingRight;

    private RaycastHit2D _hit;
    private Vector2 _inputAxis;
    private Timer inmunnity;

    private bool selectorActivo = false;

    // Start is called before the first frame update
    void Start()
    {
        monedas = 0;
        scriptUI = UI.GetComponent<UI_Controller>();
        _box = transform.GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _renderer = GetComponent<Renderer>();
        _original = _renderer.material.color;
        _box = GetComponent<BoxCollider2D>();
        health = Max_health;
        inmunnity = gameObject.AddComponent<Timer>();
        inmunnity.Duration =0.5f;
        inmunnity.Run();
        scriptUI.setVida((int)Max_health);
    }

    // Update is called once per frame
    void Update()
    {
        if (_box.IsTouchingLayers()&&inmunnity.Finished)
        {
            takeDamage(1);
            inmunnity.Run();
            //Debug.Log("vida: "+healt);
            if(health ==0)
            {
                Debug.Log("U DED");
                OnBecameInvisible();
                health = Max_health;
                scriptUI.setVida((int)Max_health);
            }
        }

        _grounded = IsGrounded();
        
        if (!playerCanMove)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _fixed = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _fixed = false;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Ability(1);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Ability(2);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(!selectorActivo)
            {
                scriptUI.activarUISelector();
                selectorActivo = true;
                StartCoroutine(deactivateUISelector());
            }
            else
            {
                StopCoroutine(deactivateUISelector());
                scriptUI.desactivarUISelector();
                selectorActivo = false;
            }
        }
        

        //_inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_grounded)
        {
            _jump = true;
            _walk = true;
            _dash = true;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(horizontalInput, 0f);

    }

    IEnumerator deactivateUISelector()
    {
        yield return new WaitForSecondsRealtime((float)1.5);
        scriptUI.desactivarUISelector();
        selectorActivo = false;
    }
    void takeDamage(int pDamage)
    {
        health -= pDamage;
        scriptUI.reducirPuntosVida(pDamage);
        Debug.Log("Player took: " + pDamage + " damage");
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

        if (!_fixed)
        {
            if (Input.GetKeyDown(KeyCode.E) && _dash)
            {
                Debug.Log("dashprron");
                _rigidbody.velocity = new Vector2(_movement.x * dashSpeed, _rigidbody.velocity.y);
                _dash = false;

            }
            else
            {
                float limitedSpeed = Mathf.Clamp(_movement.normalized.x * speed, -maxSpeed, maxSpeed);
                _rigidbody.velocity = new Vector2(limitedSpeed, _rigidbody.velocity.y);
            }
        }

    }

    private void LateUpdate()
    {
        setColor();
        _animator.SetBool("Grounded", _grounded);
        _animator.SetBool("Idle", _movement == Vector2.zero || _fixed ) ;

        if(_movement.x > 0f && !_facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            _facingRight = true;
        }
        else if(_movement.x < 0f && _facingRight)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            _facingRight = false;
        }

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
        switch (this._weapon.selectedWeapon)
        {
            case Weapon.WeaponType.normalShot:
                this._renderer.material.color = _original;
                break;
            case Weapon.WeaponType.fanShot:
                this._renderer.material.color = Color.red;
                break;
            case Weapon.WeaponType.heavyShot:
                this._renderer.material.color = Color.blue;
                break;
            default:
                break;
        }
    }

    public void recolectarMonedas(int pMonedas)
    {
        monedas += pMonedas;
        scriptUI.recolectarMonedas(pMonedas);
        Debug.Log("Se recolectaron " + pMonedas + " y el total ahora es " + monedas);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundPoint.position, groundRadius, pisoLayerMask);
    }

    private void Ability(int choise)
    {
        Debug.Log("Habilidad "+choise);
        string perform="";
        if(choise == 1)
        {
            perform = this.ability1;
        }
        else if(choise == 2)
        {
            perform = this.ability2;
        }
        switch(perform)
        {
            case "SuperShot":
                Debug.Log("Habilidad "+choise+" "+perform);
                break;
            case "Invisibility":
                Debug.Log("Habilidad "+choise+" "+perform);
                break;
            case "GravityMess":
                Debug.Log("Habilidad "+choise+" "+perform);
                break;
            default:
                Debug.Log("There's no ability assigned");
                break;
        }   
    }

}
