using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarVida : MonoBehaviour
{
    private int puntosVida = 5;
    public Text textoVida;

     void Update()
    {
        textoVida.text = "VIDA: "+ puntosVida;

        if (Input.GetKeyDown(KeyCode.Space)) puntosVida-- ;
    }
}
