using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBoxMinion1 : _Enemy
{
    [Header("Unique to Bad Box Minion 1")]
    public float timeUntilDeath;

    public override void Behaviour()
    {
        transform.position = new Vector2(transform.position.x - speed, transform.position.y);

        timeUntilDeath -= Time.deltaTime;
        if (timeUntilDeath <= 0)
        {
            TakeDamage(1);
        }
    }
}
