using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    public Transform lanzadorDeObjetos;

    // Update is called once per frame
    void Update()
    {
        //Se usa para disparar, por ahora se usa la tecla "0" del numpad
        if (Input.GetButtonDown("[0]"))
        {
            dispararObjeto();
        }   
    }

    void dispararObjeto()
    {

    }
}
