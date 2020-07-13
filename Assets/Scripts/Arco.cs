using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    public Transform lanzadorDeObjetos;
    public GameObject proyectil;

    // Update is called once per frame
    void Update()
    {
        //Se usa para disparar, por ahora se usa la tecla "0" del numpad
        if (Input.GetKeyDown("[0]"))
        {
            dispararObjeto();
        }   
    }

    void dispararObjeto()
    {
        Debug.Log("disparar objeto");
        Instantiate(proyectil, lanzadorDeObjetos.position, lanzadorDeObjetos.rotation);
    }
}
