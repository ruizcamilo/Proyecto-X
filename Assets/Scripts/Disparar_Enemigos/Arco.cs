using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    public float timer = 2f ;
    public Transform lanzadorDeObjetos;
    public GameObject proyectil;
    private bool shooting = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            dispararObjeto();
        }
        Debug.Log("Entrar al update");
        //Dispara el proyectil cada n segundos.
        if(shooting == false)
        {
            Debug.Log("Entro a shooting");
            StartCoroutine(shootingTimerRoutine());
        }
    }

    //Corutina para disparar el proyectil cada n segundos definido por el timer;
    IEnumerator shootingTimerRoutine()
    {
        shooting = true;
        dispararObjeto();
        yield return new WaitForSecondsRealtime(timer);
        shooting = false;
    }

    void dispararObjeto()
    {
        Debug.Log("disparar objeto");
        Instantiate(proyectil, lanzadorDeObjetos.position, lanzadorDeObjetos.rotation);
    }
}
