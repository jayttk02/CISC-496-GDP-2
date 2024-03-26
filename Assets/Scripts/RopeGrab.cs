using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class RopeGrab : MonoBehaviour
{
    public GameObject end;
    public bool activate = false;
    // Start is called before the first frame update
    void Start()
    {
        end = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameObject.Find("Player").GetComponent<PlayerMovement>().isgrabbing());
        if (activate){
            GameObject.Find("Player").transform.position = end.transform.position;
        } else {
            GetComponent<BoxCollider2D>().enabled = true;
        }

        if (GameObject.Find("Player") == null) { }  // used to prevent errors
        else if (!GameObject.Find("Player").GetComponent<PlayerMovement>().isgrabbing()) {
            activate = false;
        }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.name == "Player" && GameObject.Find("Player").GetComponent<PlayerMovement>().isgrabbing()) {
            activate = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    void OnCollisionStay2D (Collision2D collision) {
        if (collision.gameObject.name == "Player" && GameObject.Find("Player").GetComponent<PlayerMovement>().isgrabbing()) {
            activate = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
