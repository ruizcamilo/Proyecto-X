using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public GameObject textoVida;
    MostrarVida scriptVida;
    public GameObject textoMonedas;
    MostrarMonedas scriptMonedas;


    // Start is called before the first frame update
    void Awake()
    {
        textoVida = this.transform.GetChild(0).gameObject;
        textoMonedas = this.transform.GetChild(1).gameObject;
        scriptVida = textoVida.GetComponent<MostrarVida>();
        scriptMonedas = textoMonedas.GetComponent<MostrarMonedas>();
    }

    public void setVida(int pPuntos)
    {
        Debug.Log("Ya debería existir script vida"+ (scriptVida != null));
        scriptVida.setPuntosVida(pPuntos);
    }

    public void reducirPuntosVida(int pPuntos)
    {
        scriptVida.reducirPuntosVida(pPuntos);
    }

    public void recolectarMonedas (int pMonedas)
    {
        scriptMonedas.recolectarMonedas(pMonedas);
    }
}
