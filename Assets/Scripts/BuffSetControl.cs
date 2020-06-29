using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSetControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            PlayerController player = other.transform.GetComponent<PlayerController>();
            if (player != null) {
                int a = Random.Range(0, 2);
                if (a == 0) {
                    Debug.Log("Speed powerup");
                    player.SpeedupActive();
                    Destroy(this.gameObject);
                } else {
                    Debug.Log("Slow Fall powerup");
                    player.LeafFalling();
                    Destroy(this.gameObject);
                }
            }
            
        }
        
    }

    
}
