using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject following;
    public float xOffset;
    public float yOffset;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(following.transform.position.x + xOffset, following.transform.position.y + yOffset, following.transform.position.z);
    }

}
