using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitbox : MonoBehaviour
{
    public bool isEnemyBullet;

    void OnTriggerEnter2D(Collider2D col)
    {
        print(col.tag);
        if (col.tag == "Enemy")
        {
            col.GetComponent<_Enemy>().BulletCollision(1);
            Destroy(this.gameObject);
        }
        else if (col.tag == "Radius")
        {
            //col.transform.parent.GetComponent<_Enemy>().BulletCollision(1);
        }
        else if (col.tag == "Projectile")
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
    }

}
