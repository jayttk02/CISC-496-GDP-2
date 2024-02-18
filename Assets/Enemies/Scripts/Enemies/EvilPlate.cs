using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EvilPlate : _Enemy
{
    [Header("Unique to Evil Plate")]

    [Tooltip("Number denotes the point that the enemy is travelling towards.")] public int currentPointIndex;
    [Tooltip("The list of coordinates that the enemy travels towards.")] public List<Vector3> travelPoints;

    [Space(10)]
    [Tooltip("The direction towards the next point.")] public Vector3 direction;
    [Tooltip("The speed at which the enemy travels horizontally.")] public float speedX;
    [Tooltip("The speed at which the enemy travels vertically.")] public float speedY;

    [Space(10)]
    [Tooltip("Checks if next position has been reached by enemy.")] public bool stopCheckX;
    [Tooltip("Checks if next position has been reached by enemy.")] public bool stopCheckY;

    [Space(10)]
    [Tooltip("The amount of time that the enemy pauses at the travelPoints.")]  public int stopDurationMax;
                                                                                public int stopDurationLeft;

    public override void Start()
    {
        base.Start();
        FindSpeed();
    }

    public override void Behaviour()
    {
        if (stopDurationLeft == 0)
        {
            transform.position = new Vector2(transform.position.x + speedX, transform.position.y + speedY);

            stopCheckX = ((travelPoints[currentPointIndex].x >= 0) && (transform.position.x >= travelPoints[currentPointIndex].x)) || ((travelPoints[currentPointIndex].x < 0) && (transform.position.x <= travelPoints[currentPointIndex].x));
            stopCheckY = ((travelPoints[currentPointIndex].y >= 0) && (transform.position.y >= travelPoints[currentPointIndex].y)) || ((travelPoints[currentPointIndex].y < 0) && (transform.position.y <= travelPoints[currentPointIndex].y));

            if (stopCheckX && stopCheckY)
            {
                transform.position = travelPoints[currentPointIndex];

                currentPointIndex++;
                if (currentPointIndex >= travelPoints.Count)
                {
                    currentPointIndex = 0;
                }
                stopDurationLeft = stopDurationMax;
            }
        }
        else
        {
            stopDurationLeft--;
            if (stopDurationLeft == 0)
            {
                FindSpeed();
                stopCheckX = false;
                stopCheckY = false;
            }
        }
    }

    void FindSpeed()
    {
        direction = (travelPoints[currentPointIndex] - transform.position).normalized;
        speedX = speed * direction.x;
        speedY = speed * direction.y;
    }
}
