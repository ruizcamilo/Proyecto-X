using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectSystem
{
    private Weapon weaponPlayer;

    private List<SelectorSystem> weaponsList;

    public enum WeaponType
    {
        NormalShot,
        FanShot,
        HeavyShot
    }

    public WeaponSelectSystem(Weapon weaponPlayer)
    {
        this.weaponPlayer = weaponPlayer;
        weaponsList = new List<SelectorSystem>();

        weaponsList.Add(new SelectorSystem
        {
            weaponType = WeaponType.NormalShot,
            activateWeaponAction = () => weaponPlayer.setWeaponType(Weapon.WeaponType.normalShot)
        });

        weaponsList.Add(new SelectorSystem
        {
            weaponType = WeaponType.FanShot,
            activateWeaponAction = () => weaponPlayer.setWeaponType(Weapon.WeaponType.fanShot)
        });

        weaponsList.Add(new SelectorSystem
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

    public List<SelectorSystem> GetSelectorSystemsList()
    {
        return weaponsList;
    }

    public class SelectorSystem
    {
        public WeaponType weaponType;

        public Action activateWeaponAction;

        public Sprite getSprite()
        {
            switch (weaponType)
            {
                default:
                case WeaponType.NormalShot:
                    break;
            }
        }
    }
}
