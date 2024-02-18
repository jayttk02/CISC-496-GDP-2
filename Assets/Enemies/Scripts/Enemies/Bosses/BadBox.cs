using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBox : _Boss
{

    [Header("Unique to Bad Box")]

    public int direction = -1;      // direction enemy is currently traveling 
    public float minimumXValue;     // furthest distance to the left the enemy can travel
    public float maximumXValue;     // furthest distance to the right the enemy can travel

    [Space(10)]
    public float minion1SpawnTimeLeft;      // timer for spawning ground minion
    public float minion2SpawnTimeLeft;      // timer for spawning air minion
    
    [Space(10)]
    public GameObject minion1;
    public GameObject minion2;

    // Update is called once per frame
    void Update()
    {
        if (!isInIntro)     // if boss is in intro, doesn't do anything
        {
            transform.position = new Vector2(transform.position.x + (speed * direction), transform.position.y);     // travels in the direction of direction value
            if (transform.position.x <= minimumXValue || transform.position.x >= maximumXValue) {                   // when furthest point has been reached, go other way
                direction = direction * -1;
                _animator.SetInteger("direction", direction);
            }

            minion1SpawnTimeLeft -= Time.deltaTime;
            if (minion1SpawnTimeLeft <= 0)      // once minion spawn time is <= 0, spawn and reset timer
            {
                Instantiate(minion1, new Vector2(transform.position.x - 3, transform.position.y - 2), Quaternion.identity);
                minion1SpawnTimeLeft = Random.Range(5.0f, 12.0f);
            }

            minion2SpawnTimeLeft -= Time.deltaTime;
            if (minion2SpawnTimeLeft <= 0)
            {
                Instantiate(minion2, new Vector2(transform.position.x - 1.65f, transform.position.y + 3.15f), Quaternion.identity);
                minion2SpawnTimeLeft = Random.Range(2.0f, 6.0f);
            }
        }
    }
}
