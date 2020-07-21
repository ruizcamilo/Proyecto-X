using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    private Timer[] _timers;
    private Transform _firePoint;

    [HideInInspector]
    public int type;

    public enum WeaponType
    {
        normalShot,
        fanShot,
        heavyShot
    }

    private int numWeapons = System.Enum.GetValues(typeof(WeaponType)).Length;
    public WeaponType selectedWeapon= WeaponType.normalShot;

    public GameObject bulletPrefab;
    public GameObject bigBulletPrefab;
    public GameObject shooter;
    public LayerMask enemyLayerMask;

    private GameObject _superShot;
    private Vector2 _direction;
    private int _facingRight;

    public int maxWeapons;
    public float[] times ={0.3f, 2f, 1f};

    public float radius = 0.25f;
    public float upCenter = 0.5f;

    private void Awake()
    {
        _firePoint = transform.Find("FirePoint");
        _timers = new Timer[numWeapons];
        for(int i=0; i<numWeapons; i++)
        {
            _timers[i] = gameObject.AddComponent<Timer>();
            _timers[i].Duration = times[i];
        }
        _superShot = transform.Find("SuperShot").gameObject;
        
    }


    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    selectedWeapon = WeaponType.normalShot;
        //}
        //else if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    selectedWeapon = WeaponType.fanShot;
        //}
        //else if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    selectedWeapon = WeaponType.heavyShot;
        //}
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }
        
        _direction = new Vector2(Input.GetAxisRaw("Horizontal")*_facingRight, Input.GetAxisRaw("Vertical"));
        if (transform.rotation.y == -1)
            _facingRight = -1;
        else
            _facingRight = 1;

    }

    private void LateUpdate()
    {
        if(_direction.x == 0 && _direction.y != 1)
        {
            _direction = new Vector2(1, 0);
        }
        _firePoint.localPosition = _direction * radius;
    }

    public void setWeaponType (WeaponType weapontype)
    {
        selectedWeapon = weapontype;
        Debug.Log("Setting weapon type to: " + weapontype);
    }

    IEnumerator Shoot()
    {
        // Shoot if timer is finished
        if (_timers[(int)selectedWeapon].Finished)
        {
            float angle = Vector2.Angle(new Vector2(_facingRight, 0), _direction);
            if (_direction.y < 0)
                angle *= -1;
            Quaternion rot = Quaternion.Euler(_firePoint.rotation.x, _firePoint.rotation.y, angle);
            shooter.GetComponent<Animator>().SetTrigger("Shoot");

            switch ((int)selectedWeapon)
            {
                case 0:
                    Debug.Log("Shooting normal shot: " + selectedWeapon);
                    Instantiate(bulletPrefab, _firePoint.position, rot);
                    break;
                case 1:
                    Debug.Log("Shooting fan shot: " + selectedWeapon);
                    Quaternion rot1 = Quaternion.Euler(_firePoint.rotation.x, _firePoint.rotation.y, angle + 30f);
                    Quaternion rot2 = Quaternion.Euler(_firePoint.rotation.x, _firePoint.rotation.y, angle - 30f);
                    Instantiate(bulletPrefab, _firePoint.position, rot1);
                    Instantiate(bulletPrefab, _firePoint.position, rot);
                    Instantiate(bulletPrefab, _firePoint.position, rot2);
                    break;
                case 2:
                    Debug.Log("Shooting heavy shot: " + selectedWeapon);
                    Instantiate(bigBulletPrefab, _firePoint.position, rot);
                    break;
                case 3:
                    _superShot.transform.localPosition = new Vector2( radius, 0);
                    _superShot.SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                    _superShot.SetActive(false);
                    break;
                default:
                    print("There's been an error");
                    break;
            }
            _timers[type].Run();

        }

    }
}
