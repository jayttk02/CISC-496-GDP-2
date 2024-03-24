using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTargetPlatformTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Bullet(Clone)"){
            Destroy(other);
            Destroy(transform.parent.gameObject);
            
        }
        
        
    }
}
