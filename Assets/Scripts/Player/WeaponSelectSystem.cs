using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectSystem
{
    private Weapon weaponPlayer;

    private List<selectorWeapon> weaponsList;

    public enum WeaponType
    {
        NormalShot,
        FanShot,
        HeavyShot
    }

    public WeaponSelectSystem(Weapon weaponPlayer)
    {
        this.weaponPlayer = weaponPlayer;
        weaponsList = new List<selectorWeapon>();

        weaponsList.Add(new selectorWeapon
        {
            weaponType = WeaponType.NormalShot,
            activateWeaponAction = () => weaponPlayer.setWeaponType(Weapon.WeaponType.normalShot)
        });

        weaponsList.Add(new selectorWeapon
        {
            weaponType = WeaponType.FanShot,
            activateWeaponAction = () => weaponPlayer.setWeaponType(Weapon.WeaponType.fanShot)
        });

        weaponsList.Add(new selectorWeapon
        {
            weaponType = WeaponType.HeavyShot,
            activateWeaponAction = () => weaponPlayer.setWeaponType(Weapon.WeaponType.heavyShot)
        });

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            weaponsList[0].activateWeaponAction();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            weaponsList[1].activateWeaponAction();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            weaponsList[2].activateWeaponAction();
        }
    }

    public class selectorWeapon
    {
        public WeaponType weaponType;

        public Action activateWeaponAction;
    }
}
