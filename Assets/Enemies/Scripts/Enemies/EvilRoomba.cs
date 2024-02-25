using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilRoomba : _Enemy
{
    [Header("Unique to Evil Roomba")]

    [Tooltip("Number denotes the point that the enemy is travelling towards.")] public int currentPointIndex;
    [Tooltip("A point on the x-axis at which the roomba stops and turn around.")] public float stopPoint0;
    [Tooltip("A point on the x-axis at which the roomba stops and turn around.")] public float stopPoint1;

    public override void Behaviour()
    {
        transform.position = new Vector2(transform.position.x + (speed * transform.localScale.x), transform.position.y);    // travels to next stopPoint
        if ((currentPointIndex == 0) && (transform.position.x <= stopPoint0))   // travelling left and reached stopPoint1
        {
            currentPointIndex = 1;  // 
            transform.localScale = new Vector2(1, transform.localScale.y);  // flips enemy
        }
        else if ((currentPointIndex == 1) && (transform.position.x >= stopPoint1))
        {
            currentPointIndex = 0;  
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
    }
}
