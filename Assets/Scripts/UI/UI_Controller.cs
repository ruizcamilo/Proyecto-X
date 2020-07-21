using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public GameObject textoVida;
    MostrarVida scriptVida;
    public GameObject textoMonedas;
    MostrarMonedas scriptMonedas;
    UI_Selector scriptUISelector;

    public GameObject uiSelector;


    // Start is called before the first frame update
    void Awake()
    {
        textoVida = this.transform.GetChild(0).gameObject;
        textoMonedas = this.transform.GetChild(1).gameObject;
        scriptVida = textoVida.GetComponent<MostrarVida>();
        scriptMonedas = textoMonedas.GetComponent<MostrarMonedas>();
        uiSelector = this.transform.GetChild(2).gameObject;
        scriptUISelector = uiSelector.GetComponent<UI_Selector>();
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

    public void activarUISelector()
    {
        scriptUISelector.setUI_SelectorActive();
    }

    public void desactivarUISelector()
    {
        scriptUISelector.setUI_SelectorInactive();
    }
}
