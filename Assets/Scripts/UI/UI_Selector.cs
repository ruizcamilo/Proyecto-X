using System.Collections;
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

        this.gameObject.SetActive(false);

    }

    public void setWeaponSelectSystem (WeaponSelectSystem weaponSelectSystem)
    {
        this.weaponSelectSystem = weaponSelectSystem;
        UpdateVisual();
    }

    public void setUI_SelectorActive()
    {
        this.gameObject.SetActive(true);
    }

    public void setUI_SelectorInactive()
    {
        this.gameObject.SetActive(false);
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
            weaponSlotRectTransform.anchoredPosition = new Vector2(60f * Mathf.Sin(i*(Mathf.PI/2)),60f * Mathf.Cos(i* (Mathf.PI / 2)));
            weaponSlotTransform.Find("ItemImage").GetComponent<Image>().sprite = weapon.getSprite();
            weaponSlotTransform.Find("numberText").GetComponent<TMPro.TextMeshProUGUI>().SetText((i+1).ToString());
        }
    }
}
