using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            RectTransform weaponSlotRectTransform = weaponSlotTransform.GetComponent<RectTransform>();
            weaponSlotRectTransform.anchoredPosition = new Vector2(100f * Mathf.Sin(i*(Mathf.PI/2)),100f * Mathf.Cos(i* (Mathf.PI / 2)));
        }
    }
}
