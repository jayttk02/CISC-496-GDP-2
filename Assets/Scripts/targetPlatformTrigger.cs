using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetPlatformTrigger : MonoBehaviour
{
    

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Bullet(Clone)"){
            Destroy(other);
            Destroy(gameObject);
            
        }
        
        
    }
}
