using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetPlatformTrigger : MonoBehaviour
{
        
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Bullet(Clone)"){
            Destroy(other);
            Destroy(gameObject);
            
        }
        
        
    }
}
