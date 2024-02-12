using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 2f;
    public float xOffset = 4f;
    public float yOffset = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(player.position.x + xOffset,player.position.y + yOffset,player.position.z - 10f);
        transform.position = Vector3.Slerp(transform.position,newPos,followSpeed*Time.deltaTime);
    }
}
