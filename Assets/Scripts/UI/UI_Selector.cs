﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Selector : MonoBehaviour
{
    private Transform casilla_Template;
    private WeaponSelectSystem weaponSelectSystem;

    private void Awake()
    {
        casilla_Template = transform.Find("Casilla_Template");
        casilla_Template.gameObject.SetActive(false);

    }

    public void setWeaponSelectSystem (WeaponSelectSystem weaponSelectSystem)
    {
        this.weaponSelectSystem = weaponSelectSystem;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        List<WeaponSelectSystem.SelectorSystem> weaponsList = weaponSelectSystem.GetSelectorSystemsList();
        for (int i = 0; i<weaponsList.Count; i++ )
        {
            WeaponSelectSystem.SelectorSystem weapon = weaponsList[i];
            Transform weaponSlotTransform = Instantiate(casilla_Template, transform);
            weaponSlotTransform.gameObject.SetActive(true);
            RectTransform weaponSlotRectTransform = weaponSlotTransform.GetComponent<RectTransform>();
            weaponSlotRectTransform.anchoredPosition = new Vector2(100f * Mathf.Sin(i*(Mathf.PI/2)),100f * Mathf.Cos(i* (Mathf.PI / 2)));
            Debug.Log("weaponslottransform: " + weaponSlotTransform == null);
            Debug.Log("weapon: " + weapon == null);
            Debug.Log("weaponslottransform item image: " + weaponSlotTransform.Find("itemImage") == null);
            weaponSlotTransform.Find("itemImage").GetComponent<Image>().sprite = weapon.getSprite();
            Debug.Log("Paso por update list: " + i);
        }
    }
}