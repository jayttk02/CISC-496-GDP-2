using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBox : _Boss
{

    [Header("Unique to Bad Box")]

    bool isGrounded;

    public int direction = -1;
    public float minimumXValue;
    public float maximumXValue;

    [Space(10)]
    public float minion1SpawnTimeLeft;
    public float minion2SpawnTimeLeft;
    
    [Space(10)]
    public GameObject minion1;
    public GameObject minion2;

    // Update is called once per frame
    void Update()
    {
        if (GroundLinecast())
        {
            isGrounded = true;
            _animator.SetTrigger("isGrounded");
        }

        if (isGrounded)
        {
            transform.position = new Vector2(transform.position.x + (speed * direction), transform.position.y);
            if (transform.position.x <= minimumXValue || transform.position.x >= maximumXValue) {
                direction = direction * -1;
                _animator.SetInteger("direction", direction);
            }

            minion1SpawnTimeLeft -= Time.deltaTime;
            if (minion1SpawnTimeLeft <= 0)
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
