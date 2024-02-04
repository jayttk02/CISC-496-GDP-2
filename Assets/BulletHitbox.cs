using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitbox : MonoBehaviour
{
    public bool isEnemyBullet;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<_Enemy>().TakeDamage(1);
            Destroy(this.gameObject);
        }
    }

}
