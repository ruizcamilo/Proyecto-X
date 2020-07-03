using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            player.grounded = true;
        }
        if(col.gameObject.tag == "Platform")
        {
            player.transform.parent=col.transform;
            player.grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            player.grounded = false;
        }
        if(col.gameObject.tag == "Platform")
        {
            player.transform.parent=null;
            player.grounded = false;
        }
    }
}
