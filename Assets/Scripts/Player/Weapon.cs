using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Timer _timer;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bigBulletPrefab;
    public int type;
    public int maxWeapons;

    public float normalShoot = 0.5f;
    public float bigShoot = 3f;
    public float tripleShoot = 2f;


    private void Awake()
    {
        _timer = gameObject.AddComponent<Timer>();
        _timer.Duration = 0.1f;
        _timer.Run();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            ChangeWeapon();
        }
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    void ChangeWeapon()
    {
        this.type = (this.type + 1) % maxWeapons;
            //print("a key was pressed and also: "+ this.type);
    }
    void Shoot()
    {
        // Shoot if timer is finished
        if (_timer.Finished)
        {
            switch (this.type)
            {
                case 0:
                    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    _timer.Duration = normalShoot;
                    break;
                case 1:
                    if (firePoint.rotation.y == 0)
                    {
                        Quaternion rot1 = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.x + 30f);
                        Quaternion rot2 = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.x - 30f);
                        Instantiate(bulletPrefab, firePoint.position, rot1);
                        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                        Instantiate(bulletPrefab, firePoint.position, rot2);
                    }

                    if (firePoint.rotation.y == -1)
                    {
                        Quaternion rot1 = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.x + 150f);
                        Quaternion rot2 = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.x - 150f);
                        Instantiate(bulletPrefab, firePoint.position, rot1);
                        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                        Instantiate(bulletPrefab, firePoint.position, rot2);
                    }
                    _timer.Duration = tripleShoot;
                    break;
                case 2:
                    Instantiate(bigBulletPrefab, firePoint.position, firePoint.rotation);
                    _timer.Duration = bigShoot;
                    break;
                default:
                    print("There's been an error");
                    break;
            }
            _timer.Run();
        }
        
    }
}
