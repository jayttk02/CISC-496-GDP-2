using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotion : MonoBehaviour
{
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gun = GameObject.Find("Gun");
        GetComponent<Rigidbody2D>().velocity = new Vector3(speed * Mathf.Cos(gun.transform.eulerAngles.z * Mathf.Deg2Rad), speed * Mathf.Sin(gun.transform.eulerAngles.z * Mathf.Deg2Rad), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
