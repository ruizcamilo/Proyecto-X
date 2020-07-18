using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] private Weapon weaponPlayer;

    private WeaponSelectSystem weaponSelectSystem;

    private void Start()
    {
        weaponSelectSystem = new WeaponSelectSystem(weaponPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        weaponSelectSystem.Update();
    }
}
