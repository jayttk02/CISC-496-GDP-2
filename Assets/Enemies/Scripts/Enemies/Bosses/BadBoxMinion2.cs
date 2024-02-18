using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBoxMinion2 : _Enemy
{
    [Header("Unique to Bad Box Minion 2")]
    public int phase = 0;

    [Space(10)]
    public float maximumYValue;

    [Space(10)]
    public int minimumXValue;
    public int maximumXValue;
    public float minimumYValue;

    public override void Behaviour()
    {
        if (phase == 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed);
            if (transform.position.y >= maximumYValue)
            {
                transform.position = new Vector2(Random.Range(minimumXValue, maximumXValue), transform.position.y + speed);
                transform.localScale = new Vector2(1, -1);
                phase = 1;
            }
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (float)(speed * 0.85));
            if (transform.position.y <= minimumYValue)
            {
                TakeDamage(1);
            }
        }
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        other.gameObject.GetComponent<HealthSystem>().UpdateHealth(1);
    //    }
    //    TakeDamage(health);
    //}
}
