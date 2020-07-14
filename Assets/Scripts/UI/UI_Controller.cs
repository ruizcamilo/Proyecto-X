using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    GameObject textoVida;
    MostrarVida scriptVida;
    GameObject textoMonedas;
    MostrarMonedas scriptMonedas;


    // Start is called before the first frame update
    void Start()
    {
        textoVida = this.transform.GetChild(0).gameObject;
        textoMonedas = this.transform.GetChild(1).gameObject;
        scriptVida = textoVida.GetComponent<MostrarVida>();
        scriptMonedas = textoMonedas.GetComponent<MostrarMonedas>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            scriptVida.reducirPuntosVida(1);
        }
    }

    public void setVida(int pPuntos)
    {
        scriptVida.setPuntosVida(pPuntos);
    }

    public void reducirPuntosVida(int pPuntos)
    {
        scriptVida.reducirPuntosVida(pPuntos);
    }
}
