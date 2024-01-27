using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFridge : _Enemy
{
    [Header("Unique to Evil Fridge")]
    [Tooltip("The maximum amount of time it takes the enemy to shoot a projectile.")] public int shootTimerMax;
    [Tooltip("The amount of time before the enemy to shoots a projectile.")] public int shootTimerLeft;

    [Space(10)]
    [Tooltip("A projectile prefab that the enemy shoots.")] public GameObject projectile;

    public override void Behaviour()
    {
        shootTimerLeft--;                                       // decreases the amount of time before the next shot is made
        _animator.SetInteger("shootTimer", shootTimerLeft);     // updates the animator component so low-timer animation can play when appropriate
        if (shootTimerLeft == 0)
        {
            Instantiate(projectile, new Vector2(transform.position.x - 0.5f, transform.position.y), Quaternion.identity);   // spawns projectile when timer <= 0
            TakeDamage(1);                      // inflicts 1 health worth of recoil (to display death state)
            shootTimerLeft = shootTimerMax;     // resets current timer value to maximum
        }
    }
}
