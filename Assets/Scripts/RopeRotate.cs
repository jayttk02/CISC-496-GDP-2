using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRotate : MonoBehaviour
{
    public float speed = 5f;
    public int dir = 1;
    public RopeRotate reverse;
    public float thresh = 40;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localEulerAngles.z >= thresh && !(transform.localEulerAngles.z >= thresh+10) && dir == 1) {
            enabled = false;
            reverse.enabled = true;
        } 
        if (transform.localEulerAngles.z <= 360-thresh && !(transform.localEulerAngles.z <= 350-thresh)  && dir == -1) {
            enabled = false;
            reverse.enabled = true;
        }
        transform.Rotate(new Vector3(0,0,dir) * speed * Time.deltaTime);
    }
}
