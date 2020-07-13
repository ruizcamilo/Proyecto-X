using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Selector : MonoBehaviour
{
    private Transform casilla_Template;

    private void Awake()
    {
        casilla_Template = transform.Find("Casilla_Template");
        casilla_Template.gameObject.SetActive(false);

    }

    private void updateVisual()
    {

    }
}
