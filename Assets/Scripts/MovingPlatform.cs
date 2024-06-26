using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f){
            i++;
        }
        if(i==points.Length){
            i=0;
        }
        transform.position = Vector2.MoveTowards(transform.position,points[i].position,speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.position.y > transform.position.y)
        {
            other.transform.parent = transform;
            
            other.gameObject.GetComponent<PlayerMovement>().onMovingPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        other.transform.SetParent(null);
        other.gameObject.GetComponent<PlayerMovement>().onMovingPlatform = false;
    }

}
