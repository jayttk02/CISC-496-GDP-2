using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDamage : MonoBehaviour
{
    public bool punch = true;
    public bool kick = true;
    public bool shoot = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Bullet(Clone)" && shoot) {
            Debug.Log("Damaged");
        }
        /*if (coll.gameObject.name == "Arm" && punch) {
            Debug.Log("Damaged");
        }
        if (coll.gameObject.name == "Leg" && kick) {
            Debug.Log("Damaged");
        }*/
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Player" && kick) {
            if (coll.gameObject.GetComponent<PlayerMovement>().iskicking()) {
                Debug.Log("Damaged");
            }
        }
        if (coll.gameObject.name == "Player" && punch) {
            if (coll.gameObject.GetComponent<PlayerMovement>().ispunching()) {
                Debug.Log("Damaged");
            }
        }
    }
}
