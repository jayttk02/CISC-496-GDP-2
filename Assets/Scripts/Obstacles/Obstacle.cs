using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public string obstacle;
    public GameObject startLoc;
    private bool resetObj;
    private float duration = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (obstacle == "fall")
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            duration = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;

        //Reset Fall position if it has hit floor or player
        if (duration <= 0 && resetObj == true)
        {
            duration = 5f;
            resetObj = false;
            this.gameObject.transform.position = startLoc.transform.position;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        //Sets flag if Fall hit floor or player
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Ground") && (obstacle ==  "fall"))
        {
            resetObj = true;
        }
        
    }
}
