using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetDoorTrigger : MonoBehaviour
{
    public GameObject objectToDisappear;

    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Bullet(Clone)"){
            Destroy(gameObject);
            Destroy(objectToDisappear);
            Destroy(other);
        }
        
        
    }
}
