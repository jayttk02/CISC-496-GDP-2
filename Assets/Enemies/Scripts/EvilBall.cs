using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBall : _Enemy
{
    [Header("Unique to Evil Ball")]
    [Tooltip("If the enemy is on the ground.")] public bool isGrounded;
    [Tooltip("The maximum amount of time it takes the enemy to bounce.")] public int bounceTimerMax;    public int bounceTimerLeft;
    [Tooltip("The forward velocity of the enemy's bounce.")] public int bounceX;
    [Tooltip("The upward velocity of the enemy's bounce.")] public int bounceY;

    //public override void Start()
    //{
    //    base.Start();
    //}

    public override void Behaviour()
    {
        if (!isGrounded && GroundLinecast())
        {
            isGrounded = true;
            bounceTimerLeft = bounceTimerMax;
            _rigidbody.velocity = new Vector2(0, 0);
            _animator.SetBool("isGrounded", true);
        }
        else if (isGrounded)
        {
            bounceTimerLeft--;
            if (bounceTimerLeft <= 0)
            {
                _rigidbody.velocity = new Vector2(bounceX * transform.localScale.x, bounceY);
                isGrounded = false;
                _animator.SetBool("isGrounded", false);
                _animator.SetTrigger("jumped");
            }
        }
    }
}
