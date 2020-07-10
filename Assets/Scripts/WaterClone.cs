using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterClone : MonoBehaviour
{
    private Rigidbody2D rgb;
    public GameObject waterClone;
    //bool clone = false;
    int numClones = 0;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E) && !clone)
        if(Input.GetKeyDown(KeyCode.E) && numClones<=4)
        {
            //clone = true;
            numClones++;
            Instantiate(waterClone, firePoint.position, firePoint.rotation);
        }
    }
}
