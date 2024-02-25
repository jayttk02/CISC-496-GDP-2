using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBallArc : EvilBall
{
    [Header("Unique to Evil Ball Arc")]
    public int direction;

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
                direction *= -1;
                _animator.SetInteger("direction", direction);

                _rigidbody.velocity = new Vector2(bounceX * direction, bounceY);
                isGrounded = false;
                _animator.SetBool("isGrounded", false);
                _animator.SetTrigger("jumped");
            }
        }
    }
}
