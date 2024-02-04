using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            print("Collided with enemy");
        }
        else if (col.tag == "Projectile")
        {
            print("Collided with projectile");
        }
    }
}
