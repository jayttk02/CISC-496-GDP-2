using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBurger : _Enemy
{
    public virtual void OnCollisionTrigger2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //TakeDamage(1);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //TakeDamage(1);
        }
    }

    public override void PunchKickCollision(int strength, AttackType attackType)
    {
        if (attackType == AttackType.Kick)
        {
            TakeDamage(strength);
        }
        else
        {
            StartCoroutine(NoDamageFlash());
        }
    }

    public override void BulletCollision(int strength)
    {
        StartCoroutine(NoDamageFlash());
    }
}
