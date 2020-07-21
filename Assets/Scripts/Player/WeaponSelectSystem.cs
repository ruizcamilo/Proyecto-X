using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectSystem
{
    private Weapon weaponPlayer;

    private List<SelectorSystem> weaponsList;

    public bool selecting = false;

    public enum WeaponType
    {
        NormalShot,
        FanShot,
        HeavyShot,
        SuperShot
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

        weaponsList.Add(new SelectorSystem
        {
            weaponType = WeaponType.SuperShot,
            activateWeaponAction = () => weaponPlayer.setWeaponType(Weapon.WeaponType.superShot)
        });

    }
    public void Update()
    {
        if (selecting)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                weaponsList[0].activateWeaponAction();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                weaponsList[1].activateWeaponAction();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                weaponsList[2].activateWeaponAction();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                weaponsList[3].activateWeaponAction();
            }
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
                case WeaponType.NormalShot: return TestScript.Instance.normalShot;
                case WeaponType.FanShot: return TestScript.Instance.fanShot;
                case WeaponType.HeavyShot: return TestScript.Instance.HeavyShot;
                case WeaponType.SuperShot: return TestScript.Instance.SuperShot;
            }
        }
    }
}
