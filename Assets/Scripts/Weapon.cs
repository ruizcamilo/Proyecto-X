using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bigBulletPrefab;
    public int type;
    public int maxweapons;

    // Update is called once per frame
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
        if(this.type + 1 == maxweapons)
        {
            this.type = 0;
        } 
        else{
            this.type = this.type + 1;
        }
            //print("a key was pressed and also: "+ this.type);
    }
    void Shoot()
    {
        //shooting 
        switch (this.type)
        {
            case 0:
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                break;
            case 1:
                if(firePoint.rotation.y == 0)
                {
                    Quaternion rot1 = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.x+30f);
                    Quaternion rot2 = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.x-30f);
                    Instantiate(bulletPrefab, firePoint.position, rot1);
                    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Instantiate(bulletPrefab, firePoint.position, rot2);
                }

                if(firePoint.rotation.y == -1)
                {
                    Quaternion rot1 = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.x+120f);
                    Quaternion rot2 = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.x-120f);
                    Instantiate(bulletPrefab, firePoint.position, rot1);
                    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Instantiate(bulletPrefab, firePoint.position, rot2);
                }
                break;
            case 2:
                Instantiate(bigBulletPrefab, firePoint.position, firePoint.rotation);
                break;
            default:
                print("There's been an error");
                break;
        }
    }
}
