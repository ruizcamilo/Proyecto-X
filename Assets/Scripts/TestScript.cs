using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public static TestScript Instance { get; private set; }
    [SerializeField] private Weapon weaponPlayer;
    [SerializeField] private UI_Selector uiSelector;
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
        uiSelector.setWeaponSelectSystem(weaponSelectSystem);
    }
    IEnumerator waitCoroutine()
    {
        yield return new WaitForSecondsRealtime((float)0.5);
    }

    // Update is called once per frame
    void Update()
    {
        weaponSelectSystem.Update();
    }
}
