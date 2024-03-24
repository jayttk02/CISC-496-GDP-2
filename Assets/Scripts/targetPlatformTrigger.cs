using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetPlatformTrigger : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Bullet(Clone)"){
            Destroy(other);
            Destroy(gameObject);
            obj.SetActive(true);
            
        }
        
        
    }
}
