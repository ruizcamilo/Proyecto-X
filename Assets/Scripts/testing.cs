using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public static Testing Instance { get; private set; }
    [SerializeField] private Weapon weaponPlayer;

    public Sprite normalShot;

    public Sprite fanShot;

    public Sprite HeavyShot;

    private WeaponSelectSystem weaponSelectSystem;


    private void Awake()
    {
        Instance = this;
    }

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
