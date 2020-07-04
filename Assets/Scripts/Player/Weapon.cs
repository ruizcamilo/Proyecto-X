using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    private Timer[] _timers;
    private Transform _firePoint;

    public GameObject bulletPrefab;
    public GameObject bigBulletPrefab;

    private Vector2 _direction;
    private int _facingRight;

    public int maxWeapons;
    public int type;
    public float[] times ={0.3f, 2f, 1f};

    public float radius = 0.25f;
    public float upCenter = 0.5f;

    private void Awake()
    {
        _firePoint = transform.Find("FirePoint");
        _timers = new Timer[maxWeapons];
        for(int i=0; i<maxWeapons; i++)
        {
            _timers[i] = gameObject.AddComponent<Timer>();
            _timers[i].Duration = times[i];
            _timers[i].Run();
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            type = (type + 1) % maxWeapons;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            type = (type - 1) % maxWeapons;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        
        _direction = new Vector2(Input.GetAxisRaw("Horizontal")*_facingRight, Input.GetAxisRaw("Vertical"));
        if (transform.rotation.y == -1)
            _facingRight = -1;
        else
            _facingRight = 1;

    }

    private void LateUpdate()
    {
        if (_direction.x != 0 || _direction.y != 0)
        {
            _firePoint.localPosition = _direction*radius;
        }
        else
        {
            _firePoint.localPosition = (new Vector2(_facingRight, 0)) * radius;
        }
    }

    void Shoot()
    {
        // Shoot if timer is finished
        if (_timers[type].Finished)
        {
            float angle = Vector2.Angle(new Vector2(_facingRight, 0), _direction);
            if (_direction.y < 0)
                angle *= -1;

            Quaternion rot = Quaternion.Euler(_firePoint.rotation.x, _firePoint.rotation.y, angle);

            Vector2 instantiate;
            if (angle > 0 && angle < 180)
                instantiate = new Vector2(_firePoint.position.x, _firePoint.position.y + upCenter);
            else
                instantiate = _firePoint.position;

            switch (type)
            {
                case 0:
                    Instantiate(bulletPrefab, instantiate, rot);
                    break;
                case 1:
                    Quaternion rot1 = Quaternion.Euler(_firePoint.rotation.x, _firePoint.rotation.y, angle + 30f);
                    Quaternion rot2 = Quaternion.Euler(_firePoint.rotation.x, _firePoint.rotation.y, angle - 30f);
                    Instantiate(bulletPrefab, instantiate, rot1);
                    Instantiate(bulletPrefab, instantiate, rot);
                    Instantiate(bulletPrefab, instantiate, rot2);
                    break;
                case 2:
                    Instantiate(bigBulletPrefab, instantiate, rot);
                    break;
                default:
                    print("There's been an error");
                    break;
            }
            _timers[type].Run();
        }
        
    }
}
