using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCollectable : MonoBehaviour
{
    public AudioSource se_win;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            se_win.Play();
            print("Collected");
            //other.gameObject.GetComponent<PlayerMovement>().EndLevel();
            GameObject.Find("GameManager").GetComponent<GameManager>().Level0Clear();
            Destroy(this.gameObject);
        }
    }


}
