using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarMonedas : MonoBehaviour
{
    private int totalMonedas = 0;
    public Text textoMonedas ;

    void Update()
    {
        textoMonedas.text = "MONEDAS: " + totalMonedas;

        if (Input.GetKeyDown(KeyCode.RightAlt)) totalMonedas++;
    }
}
