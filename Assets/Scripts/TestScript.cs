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

    private bool selecting= false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        weaponSelectSystem = new WeaponSelectSystem(weaponPlayer);
        uiSelector.setWeaponSelectSystem(weaponSelectSystem);
    }
    IEnumerator waitSelectingCoroutine()
    {
        yield return new WaitForSecondsRealtime((float)1.5);
        selecting = false;
    }

    // Update is called once per frame
    void Update()
    {
        weaponSelectSystem.Update();
        if (selecting)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                weaponSelectSystem.selecting = false;
                selecting = false;
                StopCoroutine(waitSelectingCoroutine());
            }
        }else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selecting = true;
            weaponSelectSystem.selecting = true;
            StartCoroutine(waitSelectingCoroutine());
        }
    }

}
